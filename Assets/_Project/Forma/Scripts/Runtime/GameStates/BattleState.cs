using Forma.Runtime.Camera;
using Forma.Runtime.Enemies;
using Forma.Runtime.Input;
using Forma.Runtime.StateMachine.States;
using Forma.Runtime.StateMachine.Triggers;
using R3;
using UnityEngine;

namespace Forma.Runtime.GameStates
{
    public class BattleState : IState
    {
        public ITrigger OnGridSpawnRequested => _onGridSpawnRequested;

        readonly MoveInputHandler _moveInputHandler;
        readonly ToggleGridInputHandler _toggleGridInputHandler;
        readonly EnemyController _enemyController;
        readonly CameraController _cameraController;
        readonly CompositeDisposable _disposables = new();
        readonly Trigger _onGridSpawnRequested = new();

        public BattleState(MoveInputHandler moveInputHandler,
            ToggleGridInputHandler toggleGridInputHandler,
            EnemyController enemyController, CameraController cameraController)
        {
            _moveInputHandler = moveInputHandler;
            _toggleGridInputHandler = toggleGridInputHandler;
            _enemyController = enemyController;
            _cameraController = cameraController;
        }

        public void OnEnter()
        {
            _cameraController.ShowFollow();
            
            _moveInputHandler.Enable();
            _toggleGridInputHandler.Enable();

            _toggleGridInputHandler
               .OnGridToggled
               .Subscribe(SpawnGrid)
               .AddTo(_disposables);

            _enemyController.Spawn(new Vector3(5f, 1f, 0f));
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
