using System;

namespace Forma.Runtime.StateMachine.Triggers
{
    public class Trigger : ITrigger
    {
        public event Action OnFired;

        public void Fire()
        {
            OnFired?.Invoke();
        }
    }
}
