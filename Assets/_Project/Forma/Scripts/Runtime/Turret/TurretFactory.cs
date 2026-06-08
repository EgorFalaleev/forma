using Forma.Runtime.Common;
using Forma.Runtime.Core.Common;
using Forma.Runtime.Movement;
using Forma.Runtime.Turret.Configs;
using UnityEngine;

namespace Forma.Runtime.Turret
{
    public class TurretFactory
    {
        readonly ITargetProvider _targetProvider;
        readonly TurretConfig _turretConfig;

        public TurretFactory(ITargetProvider targetProvider, TurretConfig turretConfig)
        {
            _targetProvider = targetProvider;
            _turretConfig = turretConfig;
        }

        public Turret Create(Vector3 position, Transform parent)
        {
            var resource = Resources.Load<Turret>(Constants.Resources.Turret);

            Turret turret = Object.Instantiate(
                resource,
                position,
                Quaternion.identity,
                parent
            );

            var moveInput = new OffsetFollowMoveInput(
                _targetProvider,
                turret.transform,
                position - _targetProvider.Transform.position
            );

            turret.Construct(moveInput, _turretConfig);

            return turret;
        }
    }
}
