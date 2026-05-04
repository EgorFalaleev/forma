using Forma.Runtime.Core.Features.HexGrid.Data;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public class HexGridStateController
    {
        public HexGridState State => _currentState;

        HexGridState _currentState;

        public void SetState(HexGridState state)
        {
            _currentState = state;
        }
    }
}
