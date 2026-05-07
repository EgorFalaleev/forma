using Forma.Runtime.Core.Common;
using Forma.Runtime.Core.Enemy.Configs;
using Forma.Runtime.Core.Enemy.Views;
using Forma.Runtime.Core.Features.Health;
using Forma.Runtime.Core.Features.Movement;

namespace Forma.Runtime.Core.Enemy
{
    public class EnemyFactory
    {
        readonly ITargetProvider _targetProvider;
        readonly ITimeService _timeService;
        readonly EnemyConfig _enemyConfig;

        public EnemyFactory(ITargetProvider targetProvider, ITimeService timeService,
            EnemyConfig enemyConfig)
        {
            _targetProvider = targetProvider;
            _timeService = timeService;
            _enemyConfig = enemyConfig;
        }

        public Enemy Create(EnemyView enemyView)
        {
            var moveInput = new TargetFollowMoveInput(
                _targetProvider,
                origin: enemyView.transform
            );

            var movementController = new MovementController(
                moveInput,
                enemyView,
                _timeService,
                _enemyConfig.Movement
            );

            var health = new Health(_enemyConfig.Health);

            var enemy = new Enemy(movementController, health, enemyView.DamageReceiver);

            return enemy;
        }
    }
}
