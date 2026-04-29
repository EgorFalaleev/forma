using System.Collections.Generic;
using System.Linq;
using Forma.Runtime.Core.Features.HexGrid.Data;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public class HexOccupancyController
    {
        readonly HashSet<HexCubeCoordinates> _tiles;
        readonly HashSet<HexCubeCoordinates> _occupiedTiles;

        public HexOccupancyController(IEnumerable<HexCubeCoordinates> tiles)
        {
            _tiles = tiles.ToHashSet();

            _occupiedTiles = new HashSet<HexCubeCoordinates>
            {
                new(0, 0)
            };
        }

        public void Occupy(HexCubeCoordinates coordinates)
            => _occupiedTiles.Add(coordinates);

        public bool IsTileActive(HexCubeCoordinates coordinates)
            => GetTileStatus(coordinates) == TileStatus.Active;

        TileStatus GetTileStatus(HexCubeCoordinates coordinates)
        {
            if (_occupiedTiles.Contains(coordinates))
                return TileStatus.Occupied;

            HexCubeCoordinates[] tileNeighbours = coordinates.GetNeighbours();

            foreach (HexCubeCoordinates neighbourCoordinates in tileNeighbours)
            {
                if (!_tiles.Contains(neighbourCoordinates))
                    continue;

                if (_occupiedTiles.Contains(neighbourCoordinates))
                    return TileStatus.Active;
            }

            return TileStatus.Unavailable;
        }
    }
}
