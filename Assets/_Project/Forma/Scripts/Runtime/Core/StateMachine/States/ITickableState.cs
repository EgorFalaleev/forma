namespace Forma.Runtime.Core.StateMachine.States
{
    public interface ITickableState : IState
    {
        void Tick();
    }
}
