using Forma.Runtime.Core.Enemy.Abstract;
using Forma.Runtime.Core.Features.Movement;
using Forma.Runtime.Core.Features.Turret.Configs;
using Forma.Runtime.Core.Features.Turret.Views;
using UnityEngine;

namespace Forma.Runtime.Core.Features.Turret
{
    public class Turret
    {
        readonly IMovementController _movementController;
        readonly TurretConfig _turretConfig;
        readonly ITurretView _turretView;
        readonly IEnemyRegistry _enemyRegistry;
        readonly ITimeService _timeService;

        Transform _currentTarget;
        bool _isTracking;

        public Turret(IMovementController movementController, TurretConfig turretConfig,
            ITurretView turretView, IEnemyRegistry enemyRegistry,
            ITimeService timeService)
        {
            _movementController = movementController;
            _turretConfig = turretConfig;
            _turretView = turretView;
            _enemyRegistry = enemyRegistry;
            _timeService = timeService;
        }

        public void Tick()
        {
            _movementController.Move();

            if (_currentTarget != null)
            {
                HandleTarget();
            }
            else
            {
                TryAcquireTarget();
            }
        }

        void TryAcquireTarget()
        {
            _isTracking = false;

            foreach (Enemy.Enemy enemy in _enemyRegistry.Enemies)
            {
                float distanceSqr = (enemy.Transform.position
                  - _turretView.Transform.position).sqrMagnitude;

                if (distanceSqr
                 <= _turretConfig.DetectRadius * _turretConfig.DetectRadius)
                {
                    _currentTarget = enemy.Transform;
                    return;
                }
            }

            _turretView.StartIdleRotation();
        }

        void HandleTarget()
        {
            float distanceToCurrentTargetSqr =
                (_currentTarget.position - _turretView.Transform.position).sqrMagnitude;

            if (distanceToCurrentTargetSqr
             <= _turretConfig.DetectRadius * _turretConfig.DetectRadius)
            {
                if (!_isTracking)
                    _turretView.StopIdleRotation();

                _turretView.LookAtTarget(
                    _currentTarget.position,
                    _turretConfig.RotationSpeed * _timeService.DeltaTime
                );

                _isTracking = true;
            }
            else
            {
                _currentTarget = null;
                _turretView.StartIdleRotation();

                _isTracking = false;
            }
        }
    }
}
