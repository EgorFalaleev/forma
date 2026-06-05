namespace Forma.Runtime.HexGrid
{
    public class TileSelector
    {
        Tile _selectedTile;

        public void ProcessTileSelection(Tile tile)
        {
            if (_selectedTile != null && _selectedTile == tile)
            {
                Unselect();
                return;
            }

            Select(tile);
        }

        public void Unselect()
        {
            if (_selectedTile == null)
                return;

            _selectedTile.Unselect();

            _selectedTile = null;
        }

        void Select(Tile tile)
        {
            if (_selectedTile != null)
                _selectedTile.Unselect();

            _selectedTile = tile;

            tile.Select();
        }
    }
}
