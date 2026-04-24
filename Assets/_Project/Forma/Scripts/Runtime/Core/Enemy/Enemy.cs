using Forma.Runtime.Core.Features.Movement;

namespace Forma.Runtime.Core.Enemy
{
    public class Enemy
    {
        readonly IMovementController _movementController;

        public Enemy(IMovementController movementController)
        {
            _movementController = movementController;
        }
        
        public void Tick()
        {
            _movementController.Move();
        }
    }
}
