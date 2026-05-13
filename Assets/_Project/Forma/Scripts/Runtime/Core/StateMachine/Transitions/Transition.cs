using Forma.Runtime.Core.StateMachine.Predicates;
using Forma.Runtime.Core.StateMachine.States;

namespace Forma.Runtime.Core.StateMachine.Transitions
{
    public class Transition : ITransition
    {
        public IState NextState => _nextState;
        public IPredicate Condition => _condition;

        readonly IState _nextState;
        readonly IPredicate _condition;

        public Transition(IState nextState, IPredicate condition)
        {
            _nextState = nextState;
            _condition = condition;
        }
    }
}
