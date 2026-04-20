using System.Collections.Generic;
using Forma.Runtime.Common;
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
            
            Vector2Int gridSize = _hexGridConfig.GridSize;
            
            Vector2Int centerHex = gridSize / 2;
            Vector3 centerHexPosition = GetPositionForHexFromCoordinate(centerHex);
            Vector3 offset = gridCenterPosition - centerHexPosition;
            
            for (int y = 0; y < gridSize.y; y++)
            {
                for (int x = 0; x < gridSize.x; x++)
                {
                    var tileCoordinates = new Vector2Int(x, y);

                    Vector3 tilePosition =
                        GetPositionForHexFromCoordinate(tileCoordinates) + offset;

                    var tileData = new HexTileData(tileCoordinates, tilePosition);
                    
                    tiles.Add(tileData);
                }
            }

            return tiles;
        }
        
        Vector3 GetPositionForHexFromCoordinate(Vector2Int coordinate)
        {
            int column = coordinate.x;
            int row = coordinate.y;

            float width;
            float height;
            float xPosition;
            float yPosition;
            bool shouldOffset;
            float horizontalDistance;
            float verticalDistance;
            float offset;
            float size = _hexGridConfig.HexTileConfig.OuterSize;

            if (_hexGridConfig.HexTileConfig.IsFlatTopped)
            {
                shouldOffset = column % 2 == 0;
                width = 2f * size;
                height = Constants.Math.Sqrt3 * size;

                horizontalDistance = width * 3f / 4f;
                verticalDistance = height;

                offset = shouldOffset
                    ? height / 2f
                    : 0;

                xPosition = column * horizontalDistance;
                yPosition = row * verticalDistance - offset;
            }
            else
            {
                shouldOffset = row % 2 == 0;
                width = Constants.Math.Sqrt3 * size;
                height = 2f * size;

                horizontalDistance = width;
                verticalDistance = height * 3f / 4f;

                offset = shouldOffset
                    ? width / 2f
                    : 0;

                xPosition = column * horizontalDistance + offset;
                yPosition = row * verticalDistance;
            }

            return new Vector3(xPosition, 0, -yPosition);
        }
    }
}
