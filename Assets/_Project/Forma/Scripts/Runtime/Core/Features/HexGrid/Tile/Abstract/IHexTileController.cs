using Cysharp.Threading.Tasks;
using Forma.Runtime.HexGrid.Data;

namespace Forma.Runtime.Core.Features.HexGrid.Tile.Abstract
{
    public interface IHexTileController
    {
        UniTask OccupyTile(HexCubeCoordinates tileCoordinates);
    }
}
