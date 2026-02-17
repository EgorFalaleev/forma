using Forma.Runtime.Core.Features.Movement;

namespace Forma.Runtime.Core.Player
{
    public class Player
    {
        readonly MovementController _movementController;
        
        public Player(MovementController movementController)
        {
            _movementController = movementController;
        }

        public void Tick()
        {
            _movementController.Tick();
        }
    }
}