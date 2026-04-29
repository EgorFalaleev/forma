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

        public HexGridBuilder(HexGridConfig hexGridConfig)
        {
            _hexGridConfig = hexGridConfig;
        }

        public IEnumerable<HexTileData> CalculateHexGrid(Vector3 gridCenterPosition)
        {
            var tiles = new List<HexTileData>();

            IEnumerable<HexCubeCoordinates> coordinates = CalculateHexCoordinates();

            foreach (HexCubeCoordinates coordinate in coordinates)
            {
                Vector3 tilePosition =
                    GetPositionFromCube(coordinate) + gridCenterPosition;

                tiles.Add(new HexTileData(coordinate, tilePosition));
            }

            return tiles;
        }

        public IEnumerable<HexCubeCoordinates> CalculateHexCoordinates()
        {
            var coordinates = new List<HexCubeCoordinates>();

            int radius = _hexGridConfig.Radius;

            for (int q = -radius; q <= radius; q++)
            {
                int rMin = Mathf.Max(-radius, -q - radius);
                int rMax = Mathf.Min(radius, -q + radius);

                for (int r = rMin; r <= rMax; r++)
                {
                    var cubeCoord = new HexCubeCoordinates(q, r);

                    coordinates.Add(cubeCoord);
                }
            }

            return coordinates;
        }

        Vector3 GetPositionFromCube(HexCubeCoordinates coord)
        {
            float size = _hexGridConfig.HexTileConfig.OuterSize;

            float x;
            float z;

            if (_hexGridConfig.HexTileConfig.IsFlatTopped)
            {
                x = size * 1.5f * coord.Q;

                z = size
                  * (Constants.Math.Sqrt3 / 2f * coord.Q
                      + Constants.Math.Sqrt3 * coord.R);
            }
            else
            {
                x = size
                  * (Constants.Math.Sqrt3 * coord.Q
                      + Constants.Math.Sqrt3 / 2f * coord.R);

                z = size * 1.5f * coord.R;
            }

            return new Vector3(x, 0, -z);
        }
    }
}
