using System.Collections.Generic;
using Forma.Runtime.Core.Features.HexGrid.Data;
using Forma.Runtime.Core.Features.HexGrid.Grid;
using Forma.Runtime.Core.Features.HexGrid.Grid.Abstract;

namespace Forma.Runtime.Core.Features.HexGrid.Tile
{
    public class HexTileOccupancyController
    {
        readonly IHexGridRegistry _hexGridRegistry;
        readonly HashSet<HexCubeCoordinates> _occupiedTiles;

        public HexTileOccupancyController(IHexGridRegistry hexGridRegistry)
        {
            _hexGridRegistry = hexGridRegistry;

            _occupiedTiles = new HashSet<HexCubeCoordinates>
            {
                new(0, 0)
            };
        }

        public void Occupy(HexCubeCoordinates coordinates)
            => _occupiedTiles.Add(coordinates);

        public bool IsTileActive(HexCubeCoordinates coordinates)
            => GetTileStatus(coordinates) == HexTileStatus.Active;

        HexTileStatus GetTileStatus(HexCubeCoordinates coordinates)
        {
            if (_occupiedTiles.Contains(coordinates))
                return HexTileStatus.Occupied;

            HexCubeCoordinates[] tileNeighbours = coordinates.GetNeighbours();

            foreach (HexCubeCoordinates neighbourCoordinates in tileNeighbours)
            {
                if (!_hexGridRegistry.Tiles.ContainsKey(neighbourCoordinates))
                    continue;

                if (_occupiedTiles.Contains(neighbourCoordinates))
                    return HexTileStatus.Active;
            }

            return HexTileStatus.Unavailable;
        }
    }
}
