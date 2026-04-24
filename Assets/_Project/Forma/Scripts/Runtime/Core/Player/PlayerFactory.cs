using Forma.Runtime.Core.Features.Health;
using Forma.Runtime.Core.Features.Movement;
using Forma.Runtime.Core.Player.Configs;
using Forma.Runtime.Core.Player.Views;
using Forma.Runtime.Services.Input;
using Forma.Runtime.Services.TargetProvider;
using Forma.Runtime.Services.Time;
using UnityEngine;

namespace Forma.Runtime.Core.Player
{
    public class PlayerFactory
    {
        readonly IMoveInput _moveInput;
        readonly PlayerView _playerViewPrefab;
        readonly ITimeService _timeService;
        readonly PlayerConfig _playerConfig;
        readonly PlayerTargetProvider _playerTargetProvider;

        public PlayerFactory(IMoveInput moveInput, PlayerView playerViewPrefab,
            ITimeService timeService, PlayerConfig playerConfig,
            PlayerTargetProvider playerTargetProvider)
        {
            _moveInput = moveInput;
            _playerViewPrefab = playerViewPrefab;
            _timeService = timeService;
            _playerConfig = playerConfig;
            _playerTargetProvider = playerTargetProvider;
        }

        public Player Create()
        {
            PlayerView playerView = Object.Instantiate(_playerViewPrefab);
            
            _playerTargetProvider.Initialize(playerView.transform);
            
            IMovementController movementController = new MovementController(
                _moveInput,
                playerView,
                _timeService
            );

            var health = new PlayerHealth(_playerConfig.Health.MaxHealth);

            var player = new Player(
                movementController,
                health,
                playerView.DamageReceiver
            );

            return player;
        }
    }
}
