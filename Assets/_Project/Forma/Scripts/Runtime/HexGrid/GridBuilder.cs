using System.Collections.Generic;
using System.Linq;
using Forma.Runtime.Common;
using Forma.Runtime.HexGrid.Configs;
using Forma.Runtime.HexGrid.Data;
using UnityEngine;

namespace Forma.Runtime.HexGrid
{
    public class GridBuilder
    {
        public IReadOnlyDictionary<HexCubeCoordinates, Vector3> Layout => _layout;

        readonly HexGridConfig _hexGridConfig;
        readonly Dictionary<HexCubeCoordinates, Vector3> _layout;

        public GridBuilder(HexGridConfig hexGridConfig)
        {
            _hexGridConfig = hexGridConfig;

            _layout = CalculateGridLayout();
        }

        IEnumerable<HexCubeCoordinates> CalculateHexCoordinates()
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

        Dictionary<HexCubeCoordinates, Vector3> CalculateGridLayout()
        {
            return CalculateHexCoordinates()
               .ToDictionary(coord => coord, GetPositionFromCubeCoordinates);
        }

        Vector3 GetPositionFromCubeCoordinates(HexCubeCoordinates coord)
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
