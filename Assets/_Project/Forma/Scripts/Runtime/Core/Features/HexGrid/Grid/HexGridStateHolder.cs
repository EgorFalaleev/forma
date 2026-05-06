using System;
using Forma.Runtime.Core.Features.HexGrid.Data;
using R3;

namespace Forma.Runtime.Core.Features.HexGrid.Grid
{
    public class HexGridStateHolder : IDisposable
    {
        public ReadOnlyReactiveProperty<HexGridState> State => _currentState;

        readonly ReactiveProperty<HexGridState> _currentState = new(HexGridState.Hidden);

        public void SetState(HexGridState state) => _currentState.Value = state;

        public void Dispose() => _currentState.Dispose();
    }
}
