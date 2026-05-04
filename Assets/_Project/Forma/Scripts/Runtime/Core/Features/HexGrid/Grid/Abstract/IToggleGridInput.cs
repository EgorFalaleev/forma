using System;

namespace Forma.Runtime.Core.Features.HexGrid.Grid.Abstract
{
    public interface IToggleGridInput
    {
        event Action OnGridModeToggled;
    }
}