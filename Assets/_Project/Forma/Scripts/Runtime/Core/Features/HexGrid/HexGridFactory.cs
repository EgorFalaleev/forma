using System.Collections.Generic;
using Forma.Runtime.Core.Common;
using Forma.Runtime.Core.Features.HexGrid.Configs;
using Forma.Runtime.Core.Features.HexGrid.Data;
using Forma.Runtime.Core.Features.HexGrid.Views;
using Forma.Runtime.Core.Features.Turret;
using UnityEngine;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public class HexGridFactory
    {
        readonly HexGridConfig _hexGridConfig;
        readonly ITargetProvider _targetProvider;
        readonly IToggleGridInput _toggleGridInput;
        readonly IHexClickInput _hexClickInput;
        readonly ICameraProvider _cameraProvider;
        readonly IHexSelectionSetter _hexSelectionSetter;
        readonly HexTileSelector _hexTileSelector;
        readonly ITurretPlacer _turretPlacer;

        public HexGridFactory(HexGridConfig hexGridConfig, ITargetProvider targetProvider,
            IToggleGridInput toggleGridInput, IHexClickInput hexClickInput,
            ICameraProvider cameraProvider, IHexSelectionSetter hexSelectionSetter,
            HexTileSelector hexTileSelector, ITurretPlacer turretPlacer)
        {
            _hexGridConfig = hexGridConfig;
            _targetProvider = targetProvider;
            _toggleGridInput = toggleGridInput;
            _hexClickInput = hexClickInput;
            _cameraProvider = cameraProvider;
            _hexSelectionSetter = hexSelectionSetter;
            _hexTileSelector = hexTileSelector;
            _turretPlacer = turretPlacer;
        }

        public HexGrid Create()
        {
            var hexGridGo = new GameObject("HexGrid");

            var hexGridBuilder = new HexGridBuilder(_hexGridConfig);

            IEnumerable<HexCubeCoordinates> gridTilesCoordinates =
                hexGridBuilder.CalculateHexCoordinates();

            var hexViews = new Dictionary<HexCubeCoordinates, HexView>();
            var hexViewFactory = new HexViewFactory();

            foreach (HexCubeCoordinates tileCoordinates in gridTilesCoordinates)
            {
                HexView hexView = CreateTile(
                    hexViewFactory,
                    tileCoordinates,
                    hexGridGo.transform
                );

                hexViews.Add(tileCoordinates, hexView);
            }

            var hexTileRegistry = new HexTileRegistry(hexViews);

            var hexSelectionController = new HexSelectionController(
                _hexTileSelector,
                _hexSelectionSetter,
                hexTileRegistry
            );

            var hexOccupancyController =
                new HexOccupancyController(hexTileRegistry.Tiles.Keys);

            var hexGridAnimator = new HexGridAnimator(
                _hexGridConfig,
                hexTileRegistry.Tiles.Keys,
                new HexCubeCoordinates(0, 0)
            );

            var hexGridView = hexGridGo.AddComponent<HexGridView>();

            hexGridView.Initialize(hexGridAnimator, _cameraProvider.Camera);

            var hexGrid = new HexGrid(
                hexGridView,
                hexGridBuilder,
                _hexTileSelector,
                _toggleGridInput,
                _targetProvider,
                _hexClickInput,
                hexTileRegistry,
                hexOccupancyController,
                _hexGridConfig.HexTileConfig,
                _turretPlacer,
                hexSelectionController
            );

            return hexGrid;
        }

        HexView CreateTile(HexViewFactory hexViewFactory,
            HexCubeCoordinates tileCoordinates, Transform parent)
        {
            HexView hexView = hexViewFactory.Create(
                tileCoordinates,
                parent,
                _hexGridConfig.HexTileConfig
            );

            hexView.gameObject.SetActive(false);

            return hexView;
        }
    }
}
