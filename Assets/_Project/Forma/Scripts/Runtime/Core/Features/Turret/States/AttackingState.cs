using Forma.Runtime.Core.Features.Movement;
using Forma.Runtime.Core.Features.Turret.Configs;
using Forma.Runtime.Core.Features.Turret.Views;
using Forma.Runtime.Core.StateMachine.Predicates;
using Forma.Runtime.Core.StateMachine.States;

namespace Forma.Runtime.Core.Features.Turret.States
{
    public class AttackingState : IState
    {
        public IPredicate EnemyLeftRange => _enemyLeftRange;

        readonly ITurretView _turretView;
        readonly ITimeService _timeService;
        readonly TurretConfig _turretConfig;
        readonly TurretContext _turretContext;
        readonly IPredicate _enemyLeftRange;

        public AttackingState(ITurretView turretView, ITimeService timeService,
            TurretConfig turretConfig, TurretContext turretContext)
        {
            _turretView = turretView;
            _timeService = timeService;
            _turretConfig = turretConfig;
            _turretContext = turretContext;

            _enemyLeftRange =
                new FuncPredicate(() => _turretContext.CurrentTarget == null);
        }

        public void OnEnter() { }

        public void OnExit() { }

        public void Tick()
        {
            float distanceToCurrentTargetSqr = (_turretContext.CurrentTarget.position
              - _turretView.Transform.position).sqrMagnitude;

            if (distanceToCurrentTargetSqr
             <= _turretConfig.DetectRadius * _turretConfig.DetectRadius)
            {
                _turretView.LookAtTarget(
                    _turretContext.CurrentTarget.position,
                    _turretConfig.RotationSpeed * _timeService.DeltaTime
                );
            }
            else
            {
                _turretContext.CurrentTarget = null;
            }
        }
    }
}
