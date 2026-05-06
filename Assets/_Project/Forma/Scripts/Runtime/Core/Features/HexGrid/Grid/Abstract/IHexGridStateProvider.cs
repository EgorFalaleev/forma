using Forma.Runtime.Core.Features.HexGrid.Data;
using R3;

namespace Forma.Runtime.Core.Features.HexGrid.Grid.Abstract
{
    public interface IHexGridStateProvider
    {
        ReadOnlyReactiveProperty<HexGridState> State { get; }
    }
}
