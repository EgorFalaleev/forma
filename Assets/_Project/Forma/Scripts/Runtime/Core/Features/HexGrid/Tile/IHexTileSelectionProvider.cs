using Forma.Runtime.Core.Features.HexGrid.Data;

namespace Forma.Runtime.Core.Features.HexGrid.Tile
{
    public interface IHexTileSelectionProvider
    {
        HexTileSelection? SelectedTile { get; }
    }
}
