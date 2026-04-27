using Forma.Runtime.Core.Common;
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

        public TurretFactory(ITimeService timeService,
            TurretConfig turretConfig, ITargetProvider targetProvider)
        {
            _timeService = timeService;
            _turretConfig = turretConfig;
            _targetProvider = targetProvider;
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

            var turret = new Turret(turretMovementController);

            return turret;
        }
    }
}
