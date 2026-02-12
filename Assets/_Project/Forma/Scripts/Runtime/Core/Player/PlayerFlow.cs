using Forma.Runtime.Core.Features.Movement;

namespace Forma.Runtime.Core.Player
{
    public class PlayerFlow
    {
        readonly PlayerFactory _playerFactory;

        MovementController _movementController;
        
        public PlayerFlow(PlayerFactory playerFactory)
        {
            _playerFactory = playerFactory;
        }

        public void Initialize()
        {
            _movementController = _playerFactory.Create();
        }

        public void Tick()
        {
            _movementController.Tick();
        }
    }
}