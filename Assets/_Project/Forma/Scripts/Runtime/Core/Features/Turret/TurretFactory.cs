using Forma.Runtime.Core.Common;
using Forma.Runtime.Core.Enemy.Abstract;
using Forma.Runtime.Core.Features.Movement;
using Forma.Runtime.Core.Features.Turret.Configs;
using Forma.Runtime.Core.Features.Turret.Views;
using UnityEngine;

namespace Forma.Runtime.Core.Features.Turret
{
    public class TurretFactory
    {
        readonly ITimeService _timeService;
        readonly TurretConfig _turretConfig;
        readonly ITargetProvider _targetProvider;
        readonly IEnemyRegistry _enemyRegistry;

        public TurretFactory(ITimeService timeService, TurretConfig turretConfig,
            ITargetProvider targetProvider, IEnemyRegistry enemyRegistry)
        {
            _timeService = timeService;
            _turretConfig = turretConfig;
            _targetProvider = targetProvider;
            _enemyRegistry = enemyRegistry;
        }

        public Turret Create(TurretView turretView, Vector3 spawnPosition)
        {
            var turretMoveInput = new OffsetFollowMoveInput(
                _targetProvider,
                turretView.transform,
                spawnPosition - _targetProvider.Target.position
            );

            var turretMovementController = new MovementController(
                turretMoveInput,
                turretView,
                _timeService,
                _turretConfig.Movement
            );

            var turret = new Turret(
                turretMovementController,
                _turretConfig,
                turretView,
                _enemyRegistry,
                _timeService
            );

            return turret;
        }
    }
}
