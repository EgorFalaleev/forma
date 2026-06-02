using Forma.Runtime.Core.StateMachine.States;
using Forma.Runtime.Input;

namespace Forma.Runtime.GameStates
{
    public class BattleState : IState
    {
        readonly MoveInputHandler _moveInputHandler;

        public BattleState(MoveInputHandler moveInputHandler)
        {
            _moveInputHandler = moveInputHandler;
        }

        public void OnEnter()
        {
            _moveInputHandler.Enable();   
        }

        public void OnExit()
        {
        }
    }
}
