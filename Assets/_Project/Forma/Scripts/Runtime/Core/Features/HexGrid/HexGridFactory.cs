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

        public HexGridFactory(HexGridConfig hexGridConfig, ITargetProvider targetProvider,
            IToggleGridInput toggleGridInput, IHexClickInput hexClickInput,
            ICameraProvider cameraProvider, IHexSelectionSetter hexSelectionSetter)
        {
            _hexGridConfig = hexGridConfig;
            _targetProvider = targetProvider;
            _toggleGridInput = toggleGridInput;
            _hexClickInput = hexClickInput;
            _cameraProvider = cameraProvider;
            _hexSelectionSetter = hexSelectionSetter;
        }

        public HexGrid Create()
        {
            var hexGridGo = new GameObject("HexGrid");

            var hexGridBuilder = new HexGridBuilder(_hexGridConfig);

            IEnumerable<HexTileData> tiles =
                hexGridBuilder.CalculateHexGrid(_targetProvider.Target.position);

            var hexViews = new Dictionary<Vector2Int, HexView>();
            var hexViewFactory = new HexViewFactory();

            foreach (HexTileData tile in tiles)
            {
                HexView hexView = CreateTile(hexViewFactory, tile, hexGridGo.transform);

                hexViews.Add(tile.Coordinates, hexView);
            }

            var hexGridAnimator = new HexGridAnimator(_hexGridConfig);

            var hexGridView = hexGridGo.AddComponent<HexGridView>();

            hexGridView.Initialize(hexGridAnimator, hexViews, _cameraProvider.Camera);

            var hexTileAnimator =
                new HexTileAnimator(_hexGridConfig.HexTileConfig.AnimationConfig);

            var hexTileSelector = new HexTileSelector(hexTileAnimator);

            var hexGrid = new HexGrid(
                hexGridView,
                hexGridBuilder,
                hexTileSelector,
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
