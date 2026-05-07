using Forma.Runtime.Core.Features.Health;
using Forma.Runtime.Core.Features.Movement;
using Forma.Runtime.Core.Player.Configs;
using Forma.Runtime.Core.Player.Views;

namespace Forma.Runtime.Core.Player
{
    public class PlayerFactory
    {
        readonly IMoveInput _moveInput;
        readonly ITimeService _timeService;
        readonly PlayerConfig _playerConfig;

        public PlayerFactory(IMoveInput moveInput, ITimeService timeService,
            PlayerConfig playerConfig)
        {
            _moveInput = moveInput;
            _timeService = timeService;
            _playerConfig = playerConfig;
        }

        public Player Create(PlayerView playerView)
        {
            IMovementController movementController = new MovementController(
                _moveInput,
                playerView,
                _timeService,
                _playerConfig.Movement
            );

            var health = new Health(_playerConfig.Health);

            var player = new Player(
                movementController,
                health,
                playerView.DamageReceiver
            );

            return player;
        }
    }
}
