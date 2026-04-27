using System.Collections.Generic;
using Forma.Runtime.Core.Common;
using Forma.Runtime.Core.Features.HexGrid.Configs;
using Forma.Runtime.Core.Features.HexGrid.Data;
using Forma.Runtime.Core.Features.HexGrid.Views;
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

        public HexGridFactory(HexGridConfig hexGridConfig, ITargetProvider targetProvider,
            IToggleGridInput toggleGridInput, IHexClickInput hexClickInput,
            ICameraProvider cameraProvider, IHexSelectionSetter hexSelectionSetter,
            HexTileSelector hexTileSelector)
        {
            _hexGridConfig = hexGridConfig;
            _targetProvider = targetProvider;
            _toggleGridInput = toggleGridInput;
            _hexClickInput = hexClickInput;
            _cameraProvider = cameraProvider;
            _hexSelectionSetter = hexSelectionSetter;
            _hexTileSelector = hexTileSelector;
        }

        public HexGrid Create()
        {
            var hexGridGo = new GameObject("HexGrid");

            var hexGridBuilder = new HexGridBuilder(_hexGridConfig);

            IEnumerable<HexTileData> tiles =
                hexGridBuilder.CalculateHexGrid(_targetProvider.Target.position);

            var hexViews = new Dictionary<HexCubeCoord, HexView>();
            var hexViewFactory = new HexViewFactory();

            foreach (HexTileData tile in tiles)
            {
                HexView hexView = CreateTile(hexViewFactory, tile, hexGridGo.transform);
                hexViews.Add(tile.Coordinates, hexView);
            }

            var hexGridAnimator = new HexGridAnimator(
                _hexGridConfig, hexViews.Keys, hexGridBuilder.CenterCoord
            );

            var hexGridView = hexGridGo.AddComponent<HexGridView>();

            hexGridView.Initialize(hexGridAnimator, hexViews, _cameraProvider.Camera);

            var hexGrid = new HexGrid(
                hexGridView,
                hexGridBuilder,
                _hexTileSelector,
                _toggleGridInput,
                _targetProvider,
                _hexClickInput,
                _hexSelectionSetter
            );

            return hexGrid;
        }

        HexView CreateTile(HexViewFactory hexViewFactory, HexTileData tile,
            Transform parent)
        {
            HexView hexView = hexViewFactory.Create(
                tile,
                parent,
                _hexGridConfig.HexTileConfig
            );

            hexView.gameObject.SetActive(false);

            return hexView;
        }
    }
}
