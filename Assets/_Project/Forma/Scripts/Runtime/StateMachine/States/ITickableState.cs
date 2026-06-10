namespace Forma.Runtime.StateMachine.States
{
    public interface ITickableState : IState
    {
        void Tick();
    }
}
