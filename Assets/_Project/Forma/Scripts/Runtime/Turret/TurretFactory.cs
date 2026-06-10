using Forma.Runtime.Common;
using Forma.Runtime.Movement;
using Forma.Runtime.Player;
using Forma.Runtime.Turret.Configs;
using UnityEngine;

namespace Forma.Runtime.Turret
{
    public class TurretFactory
    {
        readonly IPlayerProvider _playerProvider;
        readonly TurretConfig _turretConfig;

        public TurretFactory(IPlayerProvider playerProvider, TurretConfig turretConfig)
        {
            _playerProvider = playerProvider;
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
                _playerProvider,
                turret.transform,
                position - _playerProvider.Transform.position
            );

            turret.Construct(moveInput, _turretConfig);

            return turret;
        }
    }
}
