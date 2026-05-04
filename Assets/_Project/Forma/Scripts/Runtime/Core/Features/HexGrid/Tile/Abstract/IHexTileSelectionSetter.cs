using Forma.Runtime.Core.Features.HexGrid.Data;

namespace Forma.Runtime.Core.Features.HexGrid.Tile.Abstract
{
    public interface IHexTileSelectionSetter
    {
        void SetSelection(HexTileSelection selection);
        void ClearSelection();
    }
}
