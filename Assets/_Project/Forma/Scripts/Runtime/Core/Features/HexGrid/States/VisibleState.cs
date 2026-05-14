using Forma.Runtime.Core.Features.HexGrid.Grid.Abstract;
using Forma.Runtime.Core.Features.HexGrid.Tile.Abstract;
using Forma.Runtime.Core.StateMachine.States;
using Forma.Runtime.Core.StateMachine.Triggers;

namespace Forma.Runtime.Core.Features.HexGrid.States
{
    public class VisibleState : IState
    {
        public ITrigger OnGridDespawnRequested => _onGridDespawnRequested;
        
        readonly IHexTileDeselector _hexTileDeselector;
        readonly IToggleGridInput _toggleGridInput;
        readonly Trigger _onGridDespawnRequested;

        public VisibleState(IHexTileDeselector hexTileDeselector,
            IToggleGridInput toggleGridInput)
        {
            _hexTileDeselector = hexTileDeselector;
            _toggleGridInput = toggleGridInput;

            _onGridDespawnRequested = new Trigger();
        }

        public void OnEnter()
        {
            _hexTileDeselector.Cleanup();

            _toggleGridInput.OnGridModeToggled += OnGridModeToggled;
        }

        public void OnExit()
        {
            _toggleGridInput.OnGridModeToggled -= OnGridModeToggled;
        }

        public void Tick() { }

        void OnGridModeToggled()
        {
            _onGridDespawnRequested.Fire();
        }
    }
}
