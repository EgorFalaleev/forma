using Forma.Runtime.Common;
using Forma.Runtime.Movement;
using Forma.Runtime.Player;
using Forma.Runtime.Projectiles;
using Forma.Runtime.Timer;
using Forma.Runtime.Turret.Configs;
using UnityEngine;

namespace Forma.Runtime.Turret
{
    public class TurretFactory
    {
        readonly IPlayerProvider _playerProvider;
        readonly TurretConfig _turretConfig;
        readonly ProjectileFactory _projectileFactory;
        readonly TimerSystem _timerSystem;

        public TurretFactory(IPlayerProvider playerProvider, TurretConfig turretConfig,
            ProjectileFactory projectileFactory, TimerSystem timerSystem)
        {
            _playerProvider = playerProvider;
            _turretConfig = turretConfig;
            _projectileFactory = projectileFactory;
            _timerSystem = timerSystem;
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

            turret.Construct(moveInput, _turretConfig, _projectileFactory, _timerSystem);

            return turret;
        }
    }
}
