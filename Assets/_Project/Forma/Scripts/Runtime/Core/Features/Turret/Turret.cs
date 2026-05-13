using Forma.Runtime.Core.Features.Movement;

namespace Forma.Runtime.Core.Features.Turret
{
    public class Turret
    {
        readonly IMovementController _movementController;
        readonly StateMachine.StateMachine _stateMachine;

        public Turret(IMovementController movementController,
            StateMachine.StateMachine stateMachine)
        {
            _movementController = movementController;
            _stateMachine = stateMachine;
        }

        public void Tick()
        {
            _movementController.Move();

            _stateMachine.Tick();
        }
    }
}
