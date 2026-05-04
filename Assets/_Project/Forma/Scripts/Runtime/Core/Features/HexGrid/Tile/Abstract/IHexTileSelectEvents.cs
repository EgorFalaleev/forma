using System;
using Forma.Runtime.Core.Features.HexGrid.Views;

namespace Forma.Runtime.Core.Features.HexGrid.Tile.Abstract
{
    public interface IHexTileSelectEvents
    {
        event Action<HexView> OnHexSelected;
        event Action OnHexDeselected;
    }
}
