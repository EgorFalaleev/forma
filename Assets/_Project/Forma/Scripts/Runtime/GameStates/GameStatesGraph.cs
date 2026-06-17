using Forma.Runtime.Camera;
using Forma.Runtime.Enemies;
using Forma.Runtime.HexGrid;
using Forma.Runtime.Input;
using Forma.Runtime.Player;
using Forma.Runtime.StateMachine.States;
using Forma.Runtime.Turret;

namespace Forma.Runtime.GameStates
{
    public class GameStatesGraph
    {
        public StateMachine.StateMachine StateMachine => _stateMachine;

        readonly StateMachine.StateMachine _stateMachine;
        readonly IState _initialState;

        public GameStatesGraph(PlayerFactory playerFactory,
            PlayerRepository playerRepository, MoveInputHandler moveInputHandler,
            ToggleGridInputHandler toggleGridInputHandler, GridController gridController,
            ClickGridTileInputHandler clickGridTileInputHandler,
            TileController tileController, TileSelector tileSelector,
            PlaceTurretInputHandler placeTurretInputHandler,
            GridRepository gridRepository, TurretController turretController,
            EnemyController enemyController, CameraController cameraController, TurretRepository turretRepository)
        {
            _stateMachine = new StateMachine.StateMachine();

            var startBattleState = new StartBattleState(
                playerFactory,
                playerRepository,
                cameraController
            );

            var battleState = new BattleState(
                moveInputHandler,
                toggleGridInputHandler,
                enemyController,
                cameraController,
                turretRepository,
                tileController
            );

            var gridState = new GridState(
                gridController,
                toggleGridInputHandler,
                clickGridTileInputHandler,
                tileController,
                tileSelector,
                placeTurretInputHandler,
                gridRepository,
                turretController,
                cameraController
            );

            _stateMachine
               .AddTransition(
                    startBattleState,
                    battleState,
                    startBattleState.OnBattleStarted
                )
               .AddTransition(battleState, gridState, battleState.OnGridSpawnRequested)
               .AddTransition(gridState, battleState, gridState.OnGridDespawned);

            _initialState = startBattleState;
        }

        public void EnterInitialState()
            => _stateMachine.SetState(_initialState);
    }
}
