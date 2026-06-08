using System.Collections.Generic;
using Forma.Runtime.HexGrid.Data;

namespace Forma.Runtime.HexGrid
{
    public class GridRepository
    {
        public IReadOnlyDictionary<HexCubeCoordinates, Tile> Tiles => _coordsToViews;
        public HexCubeCoordinates GridCenter => new(0, 0);

        readonly Dictionary<HexCubeCoordinates, Tile> _coordsToViews = new();
        readonly Dictionary<Tile, HexCubeCoordinates> _viewsToCoords = new();

        readonly HashSet<HexCubeCoordinates> _occupiedTiles = new()
        {
            new HexCubeCoordinates(0, 0)
        };

        public void Register(Tile tile, HexCubeCoordinates coordinates)
        {
            _coordsToViews[coordinates] = tile;
            _viewsToCoords[tile] = coordinates;
        }

        public void Unregister(Tile tile)
        {
            HexCubeCoordinates coordinates = _viewsToCoords[tile];

            Unregister(tile, coordinates);
        }

        public void Unregister(HexCubeCoordinates coordinates)
        {
            Tile tile = _coordsToViews[coordinates];

            Unregister(tile, coordinates);
        }

        public Tile GetView(HexCubeCoordinates coordinates)
            => _coordsToViews[coordinates];

        public HexCubeCoordinates GetCoordinates(Tile view)
            => _viewsToCoords[view];

        public bool IsTileActive(Tile tile)
        {
            HexCubeCoordinates tileCoordinates = GetCoordinates(tile);
            return IsTileActive(tileCoordinates);
        }

        public bool IsTileActive(HexCubeCoordinates coordinates)
            => GetTileStatus(coordinates) == HexTileStatus.Active;

        public void SetOccupied(HexCubeCoordinates coordinates)
            => _occupiedTiles.Add(coordinates);

        HexTileStatus GetTileStatus(HexCubeCoordinates coordinates)
        {
            if (_occupiedTiles.Contains(coordinates))
                return HexTileStatus.Occupied;

            HexCubeCoordinates[] tileNeighbours = coordinates.GetNeighbours();

            foreach (HexCubeCoordinates neighbourCoordinates in tileNeighbours)
            {
                if (!_coordsToViews.ContainsKey(neighbourCoordinates))
                    continue;

                if (_occupiedTiles.Contains(neighbourCoordinates))
                    return HexTileStatus.Active;
            }

            return HexTileStatus.Unavailable;
        }

        void Unregister(Tile tile, HexCubeCoordinates coordinates)
        {
            _coordsToViews.Remove(coordinates);
            _viewsToCoords.Remove(tile);
        }
    }
}
