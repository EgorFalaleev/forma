using Forma.Runtime.Core.Features.HexGrid.Data;
using Forma.Runtime.Core.Features.HexGrid.Tile.Abstract;

namespace Forma.Runtime.Core.Features.HexGrid.Tile
{
    public class HexTileSelectionProvider
        : IHexTileSelectionProvider,
          IHexTileSelectionSetter
    {
        public HexTileSelection? SelectedTile => _selectedTile;
        
        HexTileSelection? _selectedTile;

        public void SetSelection(HexTileSelection selection)
        {
            _selectedTile = selection;
        }

        public void ClearSelection()
        {
            _selectedTile = null;
        }
    }
}
