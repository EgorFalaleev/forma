using System;
using Forma.Runtime.Core.Features.Health;
using Forma.Runtime.Core.Features.Movement;
using Forma.Runtime.Services.Collisions;
using Forma.Runtime.Services.Input;
using Forma.Runtime.Services.Time;

namespace Forma.Runtime.Core.Player
{
    public class PlayerFactory
    {
        readonly IMoveInput _moveInput;
        readonly PlayerView _playerView;
        readonly ITimeService _timeService;

        public PlayerFactory(IMoveInput moveInput, PlayerView playerView,
            ITimeService timeService)
        {
            _moveInput = moveInput;
            _playerView = playerView;
            _timeService = timeService;
        }

        public Player Create()
        {
            var movementController =
                new MovementController(_moveInput, _playerView, _timeService);

            // todo get from config
            var health = new PlayerHealth(10);

            var collisionTriggers = new CollisionTriggers(_playerView);

            var player = new Player(movementController, health, collisionTriggers);

            return player;
        }
    }
}