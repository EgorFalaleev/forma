using Forma.Runtime.Core.Features.Movement;
using Forma.Runtime.Core.Features.Turret.Configs;
using Forma.Runtime.Core.Features.Turret.Views;
using Forma.Runtime.Core.StateMachine.States;
using Forma.Runtime.Core.StateMachine.Triggers;

namespace Forma.Runtime.Core.Features.Turret.States
{
    public class AttackingState : IState
    {
        public ITrigger OnEnemyLeftRange => _onEnemyLeftRange;

        readonly ITurretView _turretView;
        readonly ITimeService _timeService;
        readonly TurretConfig _turretConfig;
        readonly TurretContext _turretContext;
        readonly Trigger _onEnemyLeftRange;

        public AttackingState(ITurretView turretView, ITimeService timeService,
            TurretConfig turretConfig, TurretContext turretContext)
        {
            _turretView = turretView;
            _timeService = timeService;
            _turretConfig = turretConfig;
            _turretContext = turretContext;

            _onEnemyLeftRange = new Trigger();
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
                _onEnemyLeftRange.Fire();
            }
        }
    }
}
