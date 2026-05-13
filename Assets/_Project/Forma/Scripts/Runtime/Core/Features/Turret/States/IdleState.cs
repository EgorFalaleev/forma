using Forma.Runtime.Core.Enemy.Abstract;
using Forma.Runtime.Core.Features.Turret.Configs;
using Forma.Runtime.Core.Features.Turret.Views;
using Forma.Runtime.Core.StateMachine.Predicates;
using Forma.Runtime.Core.StateMachine.States;

namespace Forma.Runtime.Core.Features.Turret.States
{
    public class IdleState : IState
    {
        public IPredicate EnemyFound => _enemyFound;

        readonly ITurretView _turretView;
        readonly IEnemyRegistry _enemyRegistry;
        readonly TurretConfig _turretConfig;
        readonly TurretContext _turretContext;
        readonly FuncPredicate _enemyFound;

        public IdleState(ITurretView turretView, IEnemyRegistry enemyRegistry,
            TurretConfig turretConfig, TurretContext turretContext)
        {
            _turretView = turretView;
            _enemyRegistry = enemyRegistry;
            _turretConfig = turretConfig;
            _turretContext = turretContext;

            _enemyFound = new FuncPredicate(() => _turretContext.CurrentTarget != null);
        }

        public void OnEnter()
        {
            _turretView.StartIdleRotation();
        }

        public void OnExit()
        {
            _turretView.StopIdleRotation();
        }

        public void Tick()
        {
            foreach (Enemy.Enemy enemy in _enemyRegistry.Enemies)
            {
                float distanceSqr = (enemy.Transform.position
                  - _turretView.Transform.position).sqrMagnitude;

                if (distanceSqr
                 <= _turretConfig.DetectRadius * _turretConfig.DetectRadius)
                {
                    _turretContext.CurrentTarget = enemy.Transform;
                    return;
                }
            }
        }
    }
}
