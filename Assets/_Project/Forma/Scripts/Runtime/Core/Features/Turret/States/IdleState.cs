using Forma.Runtime.Core.Enemy.Abstract;
using Forma.Runtime.Core.Features.Turret.Configs;
using Forma.Runtime.Core.Features.Turret.Views;
using Forma.Runtime.Core.StateMachine.States;
using Forma.Runtime.Core.StateMachine.Triggers;

namespace Forma.Runtime.Core.Features.Turret.States
{
    public class IdleState : IState
    {
        public ITrigger OnEnemyFound => _onEnemyFound;

        readonly ITurretView _turretView;
        readonly IEnemyRegistry _enemyRegistry;
        readonly TurretConfig _turretConfig;
        readonly TurretContext _turretContext;
        readonly Trigger _onEnemyFound;

        public IdleState(ITurretView turretView, IEnemyRegistry enemyRegistry,
            TurretConfig turretConfig, TurretContext turretContext)
        {
            _turretView = turretView;
            _enemyRegistry = enemyRegistry;
            _turretConfig = turretConfig;
            _turretContext = turretContext;

            _onEnemyFound = new Trigger();
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
                    _onEnemyFound.Fire();
                    return;
                }
            }
        }
    }
}
