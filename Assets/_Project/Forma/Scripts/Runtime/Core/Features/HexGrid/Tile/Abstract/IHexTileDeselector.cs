using Cysharp.Threading.Tasks;

namespace Forma.Runtime.Core.Features.HexGrid.Tile.Abstract
{
    public interface IHexTileDeselector
    {
        UniTask DeselectTile();
        void Cleanup();
    }
}
