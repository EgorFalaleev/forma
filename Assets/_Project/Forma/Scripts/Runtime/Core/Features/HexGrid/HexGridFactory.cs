using System.Collections.Generic;
using Forma.Runtime.Core.Features.HexGrid.Data;
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

        public HexGridFactory(HexGridConfig hexGridConfig, ITargetProvider targetProvider,
            IToggleGridInput toggleGridInput)
        {
            _hexGridConfig = hexGridConfig;
            _targetProvider = targetProvider;
            _toggleGridInput = toggleGridInput;
        }

        public HexGrid Create()
        {
            var hexGridGo = new GameObject("HexGrid");

            var hexGridBuilder = new HexGridBuilder(_hexGridConfig);

            IEnumerable<HexTileData> tiles =
                hexGridBuilder.CalculateHexGrid(_targetProvider.Target.position);

            var hexTileFactory = new HexTileFactory();

            var hexRenderers = new Dictionary<Vector2Int, HexRenderer>();

            foreach (HexTileData tile in tiles)
            {
                HexRenderer hexView = hexTileFactory.Create(
                    tile,
                    hexGridGo.transform,
                    _hexGridConfig.HexTileConfig
                );

                hexView.gameObject.SetActive(false);

                hexRenderers.Add(tile.Coordinates, hexView);
            }

            var hexGridAnimator = new HexGridAnimator(_hexGridConfig);

            var hexGridView = hexGridGo.AddComponent<HexGridView>();

            hexGridView.Initialize(hexGridAnimator, hexRenderers);

            var hexGrid = new HexGrid(
                hexGridView,
                hexGridBuilder,
                _toggleGridInput,
                _targetProvider
            );

            return hexGrid;
        }
    }
}
