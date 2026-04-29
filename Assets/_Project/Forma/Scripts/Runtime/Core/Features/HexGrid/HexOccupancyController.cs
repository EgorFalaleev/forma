using System.Collections.Generic;
using Forma.Runtime.Core.Features.HexGrid.Data;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public class HexOccupancyController
    {
        readonly HexTileRegistry _hexTileRegistry;

        readonly HashSet<HexCubeCoordinates> _occupiedCells;

        public HexOccupancyController(HexTileRegistry hexTileRegistry)
        {
            _hexTileRegistry = hexTileRegistry;

            _occupiedCells = new HashSet<HexCubeCoordinates>
            {
                new(0, 0)
            };
        }

        public void Occupy(HexCubeCoordinates coordinates)
        {
            _occupiedCells.Add(coordinates);
        }

        public bool IsTileActive(HexCubeCoordinates coordinates)
            => GetTileStatus(coordinates) == TileStatus.Active;

        TileStatus GetTileStatus(HexCubeCoordinates coordinates)
        {
            if (_occupiedCells.Contains(coordinates))
                return TileStatus.Occupied;

            HexCubeCoordinates[] tileNeighbours = coordinates.GetNeighbours();

            foreach (HexCubeCoordinates neighbourCoordinates in tileNeighbours)
            {
                if (!_hexTileRegistry.Tiles.ContainsKey(neighbourCoordinates))
                    continue;

                if (_occupiedCells.Contains(neighbourCoordinates))
                    return TileStatus.Active;
            }

            return TileStatus.Unavailable;
        }
    }
}
