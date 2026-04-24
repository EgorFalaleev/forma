using Forma.Runtime.Core.Enemy.Configs;
using Forma.Runtime.Core.Enemy.Views;
using Forma.Runtime.Core.Features.Movement;
using Forma.Runtime.Services.TargetProvider;
using Forma.Runtime.Services.Time;
using UnityEngine;

namespace Forma.Runtime.Core.Enemy
{
    public class EnemyFactory
    {
        readonly EnemyView _enemyViewPrefab;
        readonly ITargetProvider _targetProvider;
        readonly ITimeService _timeService;
        readonly EnemyConfig _enemyConfig;

        public EnemyFactory(EnemyView enemyViewPrefab, ITargetProvider targetProvider,
            ITimeService timeService, EnemyConfig enemyConfig)
        {
            _enemyViewPrefab = enemyViewPrefab;
            _targetProvider = targetProvider;
            _timeService = timeService;
            _enemyConfig = enemyConfig;
        }

        public Enemy Create()
        {
            EnemyView enemyView = Object.Instantiate(_enemyViewPrefab);

            enemyView.Initialize(_enemyConfig.Damage.Amount);
            
            var moveInput = new EnemyMoveInput(
                _targetProvider,
                self: enemyView.transform
            );

            var movementController = new MovementController(
                moveInput,
                enemyView,
                _timeService
            );

            var enemy = new Enemy(movementController);

            return enemy;
        }
    }
}
