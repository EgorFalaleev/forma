using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Forma.Runtime.Common;
using Forma.Runtime.Core.Common;
using Forma.Runtime.Core.Features.HexGrid.Configs;
using Forma.Runtime.Core.Features.HexGrid.Data;
using Forma.Runtime.Core.Features.HexGrid.Views;
using UnityEngine;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public class HexGrid : IDisposable
    {
        readonly HexGridView _hexGridView;
        readonly HexGridBuilder _builder;
        readonly HexTileSelector _hexTileSelector;
        readonly IToggleGridInput _toggleGridInput;
        readonly ITargetProvider _targetProvider;
        readonly IHexClickInput _hexClickInput;
        readonly IHexSelectionSetter _hexSelectionSetter;
        readonly HexTileRegistry _hexTileRegistry;
        readonly HexOccupancyController _hexOccupancyController;
        readonly HexTileConfig _hexTileConfig;

        bool _isGridActive;
        bool _isGridAnimating;

        public HexGrid(HexGridView hexGridView, HexGridBuilder builder,
            HexTileSelector hexTileSelector, IToggleGridInput toggleGridInput,
            ITargetProvider targetProvider, IHexClickInput hexClickInput,
            IHexSelectionSetter hexSelectionSetter, HexTileRegistry hexTileRegistry,
            HexOccupancyController hexOccupancyController, HexTileConfig hexTileConfig)
        {
            _hexGridView = hexGridView;
            _builder = builder;
            _hexTileSelector = hexTileSelector;
            _toggleGridInput = toggleGridInput;
            _targetProvider = targetProvider;
            _hexClickInput = hexClickInput;
            _hexSelectionSetter = hexSelectionSetter;
            _hexTileRegistry = hexTileRegistry;
            _hexOccupancyController = hexOccupancyController;
            _hexTileConfig = hexTileConfig;

            _toggleGridInput.OnGridModeToggled += OnToggleGrid;
            _hexClickInput.OnClicked += OnHexTileClicked;
            _hexTileSelector.OnHexSelected += OnHexSelected;
            _hexTileSelector.OnHexDeselected += OnHexDeselected;
        }

        public void Dispose()
        {
            _toggleGridInput.OnGridModeToggled -= OnToggleGrid;
            _hexClickInput.OnClicked -= OnHexTileClicked;
            _hexTileSelector.OnHexSelected -= OnHexSelected;
            _hexTileSelector.OnHexDeselected -= OnHexDeselected;
        }

        void OnToggleGrid()
        {
            ToggleGrid()
               .Forget();
        }

        void OnHexTileClicked(Vector2 screenPosition)
        {
            TryClickHexTile(screenPosition)
               .Forget();
        }

        void OnHexSelected(HexView hexView)
        {
            _hexSelectionSetter.SetSelectedHexPosition(hexView.transform.position);
        }

        void OnHexDeselected()
        {
            _hexSelectionSetter.SetSelectedHexPosition(null);
        }

        async UniTaskVoid ToggleGrid()
        {
            if (_isGridAnimating)
                return;

            _isGridAnimating = true;

            _hexTileSelector.Cleanup();

            if (!_isGridActive)
            {
                IEnumerable<HexTileData> gridPositions =
                    _builder.CalculateHexGrid(_targetProvider.Target.position);

                foreach (HexTileData hexTileData in gridPositions)
                {
                    HexCubeCoordinates tileCoordinates = hexTileData.Coordinates;

                    HexView hexView = _hexTileRegistry.GetView(tileCoordinates);

                    hexView.UpdatePosition(hexTileData.Position);

                    if (!_hexOccupancyController.IsTileActive(tileCoordinates))
                        hexView.UpdateBaseColor(_hexTileConfig.InactiveColor);
                }

                await _hexGridView.SpawnGrid(_hexTileRegistry.Tiles);
            }
            else
            {
                await _hexGridView.DespawnGrid(_hexTileRegistry.Tiles);
            }

            _isGridActive = !_isGridActive;
            _isGridAnimating = false;
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
