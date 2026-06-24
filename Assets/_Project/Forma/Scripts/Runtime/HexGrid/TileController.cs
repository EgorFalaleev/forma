using Forma.Runtime.HexGrid.Data;

namespace Forma.Runtime.HexGrid
{
    public class TileController
    {
        readonly TileSelector _tileSelector;
        readonly GridRepository _gridRepository;

        public TileController(TileSelector tileSelector, GridRepository gridRepository)
        {
            _tileSelector = tileSelector;
            _gridRepository = gridRepository;
        }

        public void ProcessTileSelection(Tile tile)
        {
            HexCubeCoordinates tileCoordinates = _gridRepository.GetCoordinates(tile);

            if (!_gridRepository.IsTileActive(tileCoordinates))
            {
                return;
            }

            _tileSelector.ProcessTileSelection(tile);
        }

        public void Reset()
            => _tileSelector.Unselect();

        public void OccupyTile(Tile tile)
        {
            HexCubeCoordinates tileCoordinates = _gridRepository.GetCoordinates(tile);

            _gridRepository.SetOccupied(tileCoordinates);

            tile.PrepareInactive();

            UpdateNeighbours(tileCoordinates);
        }

        public void UnoccupyTile(HexCubeCoordinates tileCoordinates)
        {
            _gridRepository.SetUnoccupied(tileCoordinates);

            _gridRepository
               .GetView(tileCoordinates)
               .PrepareActive();

            UpdateNeighbours(tileCoordinates);
        }

        void UpdateNeighbours(HexCubeCoordinates tileCoordinates)
        {
            HexCubeCoordinates[] tileNeighbours = tileCoordinates.GetNeighbours();

            foreach (HexCubeCoordinates neighbourCoordinates in tileNeighbours)
            {
                if (_gridRepository.IsTileActive(neighbourCoordinates))
                {
                    _gridRepository
                       .GetView(neighbourCoordinates)
                       .PrepareActive();
                }
            }
        }
    }
}
