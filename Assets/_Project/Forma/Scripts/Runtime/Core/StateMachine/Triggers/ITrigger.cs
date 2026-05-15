using System;

namespace Forma.Runtime.Core.StateMachine.Triggers
{
    public interface ITrigger
    {
        event Action OnFired;
    }
}
