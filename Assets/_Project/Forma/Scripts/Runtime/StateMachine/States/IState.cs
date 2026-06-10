namespace Forma.Runtime.StateMachine.States
{
    public interface IState
    {
        void OnEnter();
        void OnExit();
    }
}
