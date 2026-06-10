using System;

namespace Forma.Runtime.StateMachine.Triggers
{
    public interface ITrigger
    {
        event Action OnFired;
    }
}
