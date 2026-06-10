using Forma.Runtime.StateMachine.States;
using Forma.Runtime.StateMachine.Triggers;

namespace Forma.Runtime.StateMachine.Transitions
{
    public interface ITransition
    {
        IState NextState { get; }
        ITrigger Trigger { get; }
    }
}
