using Forma.Runtime.Core.StateMachine;
using Forma.Runtime.Core.StateMachine.States;
using Forma.Runtime.Input;
using Forma.Runtime.Player;

namespace Forma.Runtime.GameStates
{
    public class GameStatesGraph
    {
        public StateMachine StateMachine => _stateMachine;
        
        readonly StateMachine _stateMachine;
        readonly IState _initialState;
        
        public GameStatesGraph(PlayerFactory playerFactory,
            PlayerRepository playerRepository, MoveInputHandler moveInputHandler)
        {
            _stateMachine = new StateMachine();

            var startBattleState = new StartBattleState(playerFactory, playerRepository);
            var battleState = new BattleState(moveInputHandler);

            _stateMachine.AddTransition(
                startBattleState,
                battleState,
                startBattleState.OnBattleStarted
            );

            _initialState = startBattleState;
        }

        public void EnterInitialState()
            => _stateMachine.SetState(_initialState);
    }
}
