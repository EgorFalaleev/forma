using System;
using Cysharp.Threading.Tasks;
using Forma.Runtime.Common;
using Forma.Runtime.Core.Common;
using Forma.Runtime.Core.Features.HexGrid.Configs;
using Forma.Runtime.Core.Features.HexGrid.Data;
using Forma.Runtime.Core.Features.HexGrid.Views;
using Forma.Runtime.Core.Features.Turret;
using UnityEngine;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public class HexGrid : IDisposable
    {
        readonly HexGridView _hexGridView;
        readonly HexGridBuilder _builder;
        readonly HexTileSelector _hexTileSelector;
        readonly ITargetProvider _targetProvider;
        readonly IHexClickInput _hexClickInput;
        readonly HexTileRegistry _hexTileRegistry;
        readonly HexOccupancyController _hexOccupancyController;
        readonly HexTileConfig _hexTileConfig;
        readonly ITurretPlacer _turretPlacer;
        readonly HexSelectionController _hexSelectionController;

        bool _isGridActive;
        bool _isGridAnimating;

        public HexGrid(HexGridView hexGridView, HexGridBuilder builder,
            HexTileSelector hexTileSelector, ITargetProvider targetProvider,
            IHexClickInput hexClickInput, HexTileRegistry hexTileRegistry,
            HexOccupancyController hexOccupancyController, HexTileConfig hexTileConfig,
            ITurretPlacer turretPlacer, HexSelectionController hexSelectionController)
        {
            _hexGridView = hexGridView;
            _builder = builder;
            _hexTileSelector = hexTileSelector;
            _targetProvider = targetProvider;
            _hexClickInput = hexClickInput;
            _hexTileRegistry = hexTileRegistry;
            _hexOccupancyController = hexOccupancyController;
            _hexTileConfig = hexTileConfig;
            _turretPlacer = turretPlacer;
            _hexSelectionController = hexSelectionController;

            _hexClickInput.OnClicked += OnHexTileClicked;
            _turretPlacer.OnTurretReservedTile += OnTurretReservedTile;
        }

        public void Dispose()
        {
            _hexClickInput.OnClicked -= OnHexTileClicked;
            _turretPlacer.OnTurretReservedTile -= OnTurretReservedTile;
        }

        void OnHexTileClicked(Vector2 screenPosition)
        {
            TryClickHexTile(screenPosition)
               .Forget();
        }

        void OnTurretReservedTile(HexCubeCoordinates turretCoordinates)
        {
            _hexOccupancyController.Occupy(turretCoordinates);

            HexView hexView = _hexTileRegistry.GetView(turretCoordinates);

            hexView.UpdateBaseColor(_hexTileConfig.InactiveColor);

            HexCubeCoordinates[] tileNeighbours = turretCoordinates.GetNeighbours();

            foreach (HexCubeCoordinates tileCoordinates in tileNeighbours)
            {
                if (_hexOccupancyController.IsTileActive(tileCoordinates))
                    _hexTileRegistry
                       .GetView(tileCoordinates)
                       .ResetColor();
            }
        }

        async UniTask TryClickHexTile(Vector2 screenPosition)
        {
            if (!_isGridActive || _isGridAnimating)
                return;

            if (_hexGridView.TrySelectHexTileAt(
                screenPosition,
                1 << Constants.Layers.HexGrid,
                out HexView hexView
            ))
            {
                HexCubeCoordinates tileCoordinates =
                    _hexTileRegistry.GetCoordinates(hexView);

                bool isTileActive = _hexOccupancyController.IsTileActive(tileCoordinates);

                if (isTileActive)
                    await _hexTileSelector.ClickHexTile(hexView);
            }
        }
    }
}
