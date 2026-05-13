using Forma.Runtime.Core.StateMachine.Predicates;
using Forma.Runtime.Core.StateMachine.States;

namespace Forma.Runtime.Core.StateMachine.Transitions
{
    public interface ITransition
    {
        IState NextState { get; }
        IPredicate Condition { get; }
    }
}
