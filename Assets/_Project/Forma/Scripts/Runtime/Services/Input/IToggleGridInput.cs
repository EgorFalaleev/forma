using System;

namespace Forma.Runtime.Services.Input
{
    public interface IToggleGridInput
    {
        event Action OnGridModeToggled;
    }
}