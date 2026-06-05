using Forma.Runtime.Core.StateMachine.States;
using Forma.Runtime.Core.StateMachine.Triggers;
using Forma.Runtime.Input;
using R3;

namespace Forma.Runtime.GameStates
{
    public class BattleState : IState
    {
        public ITrigger OnGridSpawnRequested => _onGridSpawnRequested;
        
        readonly MoveInputHandler _moveInputHandler;
        readonly ToggleGridInputHandler _toggleGridInputHandler;
        readonly CompositeDisposable _disposables = new();
        readonly Trigger _onGridSpawnRequested = new();

        public BattleState(MoveInputHandler moveInputHandler,
            ToggleGridInputHandler toggleGridInputHandler)
        {
            _moveInputHandler = moveInputHandler;
            _toggleGridInputHandler = toggleGridInputHandler;
        }

        public void OnEnter()
        {
            _moveInputHandler.Enable();
            _toggleGridInputHandler.Enable();

            _toggleGridInputHandler
               .OnGridToggled
               .Subscribe(SpawnGrid)
               .AddTo(_disposables);
        }

        public void OnExit()
        {
            _moveInputHandler.Disable();
            _toggleGridInputHandler.Disable();
            
            _disposables.Clear();
        }

        void SpawnGrid(Unit _)
        {
            _onGridSpawnRequested.Fire();
        }
    }
}
