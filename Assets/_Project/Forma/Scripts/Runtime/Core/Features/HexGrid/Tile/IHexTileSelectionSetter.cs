using Forma.Runtime.Core.Features.HexGrid.Data;

namespace Forma.Runtime.Core.Features.HexGrid.Tile
{
    public interface IHexTileSelectionSetter
    {
        void SetSelection(HexTileSelection selection);
        void ClearSelection();
    }
}
