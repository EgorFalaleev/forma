using System;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public interface IToggleGridInput
    {
        event Action OnGridModeToggled;
    }
}