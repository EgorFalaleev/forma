namespace Forma.Runtime.HexGrid
{
    public class TileSelector
    {
        public Tile SelectedTile => _selectedTile;
        public bool HasSelectedTile => _selectedTile != null;
        
        Tile _selectedTile;

        public void ProcessTileSelection(Tile tile)
        {
            if (HasSelectedTile && _selectedTile == tile)
            {
                Unselect();
                return;
            }

            Select(tile);
        }

        public void Unselect()
        {
            if (!HasSelectedTile)
                return;

            _selectedTile.Unselect();

            _selectedTile = null;
        }

        void Select(Tile tile)
        {
            if (HasSelectedTile)
                _selectedTile.Unselect();

            _selectedTile = tile;

            tile.Select();
        }
    }
}
