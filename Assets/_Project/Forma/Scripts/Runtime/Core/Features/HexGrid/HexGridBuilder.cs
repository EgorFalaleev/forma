using System.Collections.Generic;
using Forma.Runtime.Common;
using Forma.Runtime.Core.Features.HexGrid.Configs;
using Forma.Runtime.Core.Features.HexGrid.Data;
using UnityEngine;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public class HexGridBuilder
    {
        readonly HexGridConfig _hexGridConfig;

        public HexCubeCoord CenterCoord { get; }

        public HexGridBuilder(HexGridConfig hexGridConfig)
        {
            _hexGridConfig = hexGridConfig;
            CenterCoord = HexCubeCoord.FromOffset(
                hexGridConfig.GridSize / 2,
                hexGridConfig.HexTileConfig.IsFlatTopped
            );
        }

        public IEnumerable<HexTileData> CalculateHexGrid(Vector3 gridCenterPosition)
        {
            var tiles = new List<HexTileData>();

            Vector2Int gridSize = _hexGridConfig.GridSize;
            bool isFlatTopped = _hexGridConfig.HexTileConfig.IsFlatTopped;

            Vector3 centerHexPosition = GetPositionFromCube(CenterCoord);
            Vector3 originOffset = gridCenterPosition - centerHexPosition;

            for (int y = 0; y < gridSize.y; y++)
            {
                for (int x = 0; x < gridSize.x; x++)
                {
                    HexCubeCoord cubeCoord = HexCubeCoord.FromOffset(new Vector2Int(x, y), isFlatTopped);
                    Vector3 tilePosition = GetPositionFromCube(cubeCoord) + originOffset;
                    tiles.Add(new HexTileData(cubeCoord, tilePosition));
                }
            }

            return tiles;
        }

        Vector3 GetPositionFromCube(HexCubeCoord coord)
        {
            float size = _hexGridConfig.HexTileConfig.OuterSize;
            float x, z;

            if (_hexGridConfig.HexTileConfig.IsFlatTopped)
            {
                x = size * 1.5f * coord.Q;
                z = size * (Constants.Math.Sqrt3 / 2f * coord.Q + Constants.Math.Sqrt3 * coord.R);
            }
            else
            {
                x = size * (Constants.Math.Sqrt3 * coord.Q + Constants.Math.Sqrt3 / 2f * coord.R);
                z = size * 1.5f * coord.R;
            }

            return new Vector3(x, 0, -z);
        }
    }
}
