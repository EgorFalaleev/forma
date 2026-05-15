using System;
using Forma.Runtime.Core.Features.HexGrid.Grid.Abstract;
using Forma.Runtime.Core.Features.HexGrid.Tile.Abstract;
using Forma.Runtime.Core.StateMachine.States;
using Forma.Runtime.Core.StateMachine.Triggers;

namespace Forma.Runtime.Core.Features.HexGrid.States
{
    public class HiddenState : IState
    {
        public event Action OnBecameHidden;

        public ITrigger OnGridSpawnRequested => _onGridSpawnRequested;

        readonly IHexTileDeselector _hexTileDeselector;
        readonly IToggleGridInput _toggleGridInput;
        readonly Trigger _onGridSpawnRequested;

        public HiddenState(IHexTileDeselector hexTileDeselector,
            IToggleGridInput toggleGridInput)
        {
            _hexTileDeselector = hexTileDeselector;
            _toggleGridInput = toggleGridInput;

            _onGridSpawnRequested = new Trigger();
        }

        public void OnEnter()
        {
            _hexTileDeselector.Cleanup();

            _toggleGridInput.OnGridModeToggled += OnGridModeToggled;

            OnBecameHidden?.Invoke();
        }

        public void OnExit()
        {
            _toggleGridInput.OnGridModeToggled -= OnGridModeToggled;
        }

        public void Tick() { }

        void OnGridModeToggled()
        {
            _onGridSpawnRequested.Fire();
        }
    }
}
