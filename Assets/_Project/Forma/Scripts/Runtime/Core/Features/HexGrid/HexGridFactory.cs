using System.Collections.Generic;
using Forma.Runtime.Core.Features.HexGrid.Data;
using Forma.Runtime.Services.CameraProvider;
using Forma.Runtime.Services.Input;
using Forma.Runtime.Services.TargetProvider;
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

        public HexGridFactory(HexGridConfig hexGridConfig, ITargetProvider targetProvider,
            IToggleGridInput toggleGridInput, IHexClickInput hexClickInput,
            ICameraProvider cameraProvider)
        {
            _hexGridConfig = hexGridConfig;
            _targetProvider = targetProvider;
            _toggleGridInput = toggleGridInput;
            _hexClickInput = hexClickInput;
            _cameraProvider = cameraProvider;
        }

        public HexGrid Create()
        {
            var hexGridGo = new GameObject("HexGrid");

            var hexGridBuilder = new HexGridBuilder(_hexGridConfig);

            IEnumerable<HexTileData> tiles =
                hexGridBuilder.CalculateHexGrid(_targetProvider.Target.position);

            var hexViewFactory = new HexViewFactory();

            var hexRenderers = new Dictionary<Vector2Int, HexView>();

            foreach (HexTileData tile in tiles)
            {
                HexView hexView = hexViewFactory.Create(
                    tile,
                    hexGridGo.transform,
                    _hexGridConfig.HexTileConfig
                );

                hexView.gameObject.SetActive(false);

                hexRenderers.Add(tile.Coordinates, hexView);
            }

            var hexGridAnimator = new HexGridAnimator(_hexGridConfig);

            var hexGridView = hexGridGo.AddComponent<HexGridView>();

            hexGridView.Initialize(hexGridAnimator, hexRenderers, _cameraProvider.Camera);

            var hexTileAnimator =
                new HexTileAnimator(_hexGridConfig.HexTileConfig.AnimationConfig);

            var hexTileSelector = new HexTileSelector(hexTileAnimator);

            var hexGrid = new HexGrid(
                hexGridView,
                hexGridBuilder,
                hexTileSelector,
                _toggleGridInput,
                _targetProvider,
                _hexClickInput
            );

            return hexGrid;
        }
    }
}
