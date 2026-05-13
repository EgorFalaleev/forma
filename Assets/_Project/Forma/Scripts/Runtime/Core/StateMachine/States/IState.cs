namespace Forma.Runtime.Core.StateMachine.States
{
    public interface IState
    {
        void OnEnter();
        void OnExit();
        void Tick();
    }
}
