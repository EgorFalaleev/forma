using Forma.Runtime.Core.Features.HexGrid.Data;

namespace Forma.Runtime.Core.Features.HexGrid.Tile.Abstract
{
    public interface IHexTileController
    {
        void OccupyTile(HexCubeCoordinates tileCoordinates);
    }
}
