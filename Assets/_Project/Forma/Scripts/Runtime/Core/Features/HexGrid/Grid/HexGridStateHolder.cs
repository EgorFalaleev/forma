using Forma.Runtime.Core.Features.HexGrid.Data;

namespace Forma.Runtime.Core.Features.HexGrid.Grid
{
    public class HexGridStateHolder
    {
        public HexGridState State => _currentState;

        HexGridState _currentState;

        public void SetState(HexGridState state)
        {
            _currentState = state;
        }
    }
}
