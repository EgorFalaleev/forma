using System;
using Forma.Runtime.Core.Features.HexGrid.Views;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public interface IHexClickInput
    {
        event Action<HexView> OnHexClicked;
    }
}
