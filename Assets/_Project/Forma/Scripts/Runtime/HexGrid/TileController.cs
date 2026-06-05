using Forma.Runtime.HexGrid.Data;

namespace Forma.Runtime.HexGrid
{
    public class TileController
    {
        readonly TileSelector _tileSelector;
        readonly GridRepository _gridRepository;

        public TileController(TileSelector tileSelector,
            GridRepository gridRepository)
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
        {
            _tileSelector.Unselect();
        }
    }
}
