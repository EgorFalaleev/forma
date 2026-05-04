using Cysharp.Threading.Tasks;

namespace Forma.Runtime.Core.Features.HexGrid.Tile
{
    public interface IHexTileDeselector
    {
        UniTask DeselectTile();
    }
}
