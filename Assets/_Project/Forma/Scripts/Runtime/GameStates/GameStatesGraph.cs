using Forma.Runtime.Core.StateMachine;
using Forma.Runtime.Core.StateMachine.States;
using Forma.Runtime.HexGrid;
using Forma.Runtime.Input;
using Forma.Runtime.Player;

namespace Forma.Runtime.GameStates
{
    public class GameStatesGraph
    {
        readonly TileController _tileController;
        public StateMachine StateMachine => _stateMachine;

        readonly StateMachine _stateMachine;
        readonly IState _initialState;

        public GameStatesGraph(PlayerFactory playerFactory,
            PlayerRepository playerRepository, MoveInputHandler moveInputHandler,
            ToggleGridInputHandler toggleGridInputHandler, GridController gridController,
            ClickGridTileInputHandler clickGridTileInputHandler,
            TileController tileController)
        {
            _tileController = tileController;
            _stateMachine = new StateMachine();

            var startBattleState = new StartBattleState(playerFactory, playerRepository);
            var battleState = new BattleState(moveInputHandler, toggleGridInputHandler);

            var gridState = new GridState(
                gridController,
                toggleGridInputHandler,
                clickGridTileInputHandler,
                tileController
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
