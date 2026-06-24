using Forma.Runtime.Camera;
using Forma.Runtime.Enemies;
using Forma.Runtime.HexGrid;
using Forma.Runtime.HexGrid.Data;
using Forma.Runtime.Input;
using Forma.Runtime.StateMachine.States;
using Forma.Runtime.StateMachine.Triggers;
using Forma.Runtime.Turret;
using R3;

namespace Forma.Runtime.GameStates
{
    public class BattleState : IState
    {
        public ITrigger OnGridSpawnRequested => _onGridSpawnRequested;

        readonly MoveInputHandler _moveInputHandler;
        readonly ToggleGridInputHandler _toggleGridInputHandler;
        readonly EnemyController _enemyController;
        readonly CameraController _cameraController;
        readonly TurretRepository _turretRepository;
        readonly TileController _tileController;
        readonly CompositeDisposable _disposables = new();
        readonly Trigger _onGridSpawnRequested = new();

        public BattleState(MoveInputHandler moveInputHandler,
            ToggleGridInputHandler toggleGridInputHandler,
            EnemyController enemyController, CameraController cameraController,
            TurretRepository turretRepository, TileController tileController)
        {
            _moveInputHandler = moveInputHandler;
            _toggleGridInputHandler = toggleGridInputHandler;
            _enemyController = enemyController;
            _cameraController = cameraController;
            _turretRepository = turretRepository;
            _tileController = tileController;
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

            _turretRepository
               .OnTurretDestroyed
               .Subscribe(OnTurretDestroyed)
               .AddTo(_disposables);

            _enemyController
               .StartSpawning()
               .Forget();
        }

        public void OnExit()
        {
            _enemyController.StopSpawning();

            _moveInputHandler.Disable();
            _toggleGridInputHandler.Disable();

            _disposables.Clear();
        }

        void SpawnGrid(Unit _)
            => _onGridSpawnRequested.Fire();

        void OnTurretDestroyed(Turret.Turret turret)
        {
            HexCubeCoordinates turretCoordinates =
                _turretRepository.GetCoordinates(turret);

            _turretRepository.Unregister(turret);

            _tileController.UnoccupyTile(turretCoordinates);
        }
    }
}
