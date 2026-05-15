using System;
using Forma.Runtime.Core.Features.HexGrid.Grid.Abstract;
using Forma.Runtime.Core.Features.HexGrid.Tile;
using Forma.Runtime.Core.StateMachine.States;
using Forma.Runtime.Core.StateMachine.Triggers;

namespace Forma.Runtime.Core.Features.HexGrid.States
{
    public class VisibleState : IState
    {
        public event Action OnBecameVisible; 
        
        public ITrigger OnGridDespawnRequested => _onGridDespawnRequested;

        readonly HexTileSelector _hexTileSelector;
        readonly IToggleGridInput _toggleGridInput;
        readonly Trigger _onGridDespawnRequested;

        public VisibleState(HexTileSelector hexTileSelector,
            IToggleGridInput toggleGridInput)
        {
            _hexTileSelector = hexTileSelector;
            _toggleGridInput = toggleGridInput;

            _onGridDespawnRequested = new Trigger();
        }

        public void OnEnter()
        {
            _hexTileSelector.Initialize();

            _toggleGridInput.OnGridModeToggled += OnGridModeToggled;
            
            OnBecameVisible?.Invoke();
        }

        public void OnExit()
        {
            _hexTileSelector.Dispose();

            _toggleGridInput.OnGridModeToggled -= OnGridModeToggled;
        }

        public void Tick() { }

        void OnGridModeToggled()
        {
            _onGridDespawnRequested.Fire();
        }
    }
}
