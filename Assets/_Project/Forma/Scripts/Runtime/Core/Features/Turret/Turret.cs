using Forma.Runtime.Core.Features.Movement;

namespace Forma.Runtime.Core.Features.Turret
{
    public class Turret
    {
        readonly IMovementController _movementController;

        public Turret(IMovementController movementController)
        {
            _movementController = movementController;
        }

        public void Tick()
        {
            _movementController.Move();
        }
    }
}
