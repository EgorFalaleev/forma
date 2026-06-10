using Forma.Runtime.StateMachine.States;
using Forma.Runtime.StateMachine.Triggers;

namespace Forma.Runtime.StateMachine.Transitions
{
    public class Transition : ITransition
    {
        public IState NextState => _nextState;
        public ITrigger Trigger => _trigger;

        readonly IState _nextState;
        readonly ITrigger _trigger;

        public Transition(IState nextState, ITrigger trigger)
        {
            _nextState = nextState;
            _trigger = trigger;
        }
    }
}
