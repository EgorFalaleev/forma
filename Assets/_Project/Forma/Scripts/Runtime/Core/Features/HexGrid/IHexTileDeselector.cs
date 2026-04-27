using Cysharp.Threading.Tasks;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public interface IHexTileDeselector
    {
        UniTask DeselectTile();
    }
}
