using Forma.Runtime.Core.Features.HexGrid.Data;
using Forma.Runtime.Core.Features.HexGrid.Tile.Abstract;

namespace Forma.Runtime.Core.Features.HexGrid.Tile
{
    public class HexTileSelectionProvider
        : IHexTileSelectionProvider,
          IHexTileSelectionSetter
    {
        public HexTileData? SelectedTile => _selectedTile;
        
        HexTileData? _selectedTile;

        public void SetSelection(HexTileData selection)
        {
            _selectedTile = selection;
        }

        public void ClearSelection()
        {
            _selectedTile = null;
        }
    }
}
