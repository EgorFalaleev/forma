using System;
using Forma.Runtime.Core.Features.HexGrid.Views;

namespace Forma.Runtime.Core.Features.HexGrid.Tile
{
    public interface IHexTileClickInput
    {
        event Action<HexView> OnHexClicked;
    }
}
