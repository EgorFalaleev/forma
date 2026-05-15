using Forma.Runtime.Core.StateMachine.States;
using Forma.Runtime.Core.StateMachine.Triggers;

namespace Forma.Runtime.Core.StateMachine.Transitions
{
    public interface ITransition
    {
        IState NextState { get; }
        ITrigger Trigger { get; }
    }
}
