using System;

namespace Forma.Runtime.Core.Features.HexGrid.Grid
{
    public interface IToggleGridInput
    {
        event Action OnGridModeToggled;
    }
}