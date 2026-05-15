using System;

namespace Forma.Runtime.Core.Features.HexGrid.Grid.Abstract
{
    public interface IHexGridEvents
    {
        event Action OnActivated;
        event Action OnDeactivated;
    }
}
