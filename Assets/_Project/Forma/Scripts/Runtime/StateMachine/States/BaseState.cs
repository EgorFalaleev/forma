namespace Forma.Runtime.StateMachine.States
{
    public abstract class BaseState : IState
    {
        public virtual void OnEnter() { }

        public virtual void OnExit() { }

        public virtual void Tick() { }
    }
}
