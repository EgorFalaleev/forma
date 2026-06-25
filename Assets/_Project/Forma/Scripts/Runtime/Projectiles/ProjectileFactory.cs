using Forma.Runtime.Common;
using Forma.Runtime.Components.MoveInput;
using Forma.Runtime.Projectiles.Configs;
using Forma.Runtime.Timer;
using UnityEngine;

namespace Forma.Runtime.Projectiles
{
    public class ProjectileFactory
    {
        readonly ProjectileConfig _projectileConfig;
        readonly TimerSystem _timerSystem;
        readonly Transform _parent;

        public ProjectileFactory(ProjectileConfig projectileConfig,
            TimerSystem timerSystem)
        {
            _projectileConfig = projectileConfig;
            _timerSystem = timerSystem;

            _parent = new GameObject("Projectiles").transform;
        }

        public Projectile Create(Vector3 position, Vector3 targetPosition)
        {
            var resource = Resources.Load<Projectile>(Constants.Resources.Projectile);

            Vector3 direction = (targetPosition - position).normalized;

            var moveInput = new ConstantDirectionMoveInput(direction);

            Quaternion rotation = Quaternion.LookRotation(direction);

            Projectile instance = Object.Instantiate(
                resource,
                position,
                rotation,
                _parent
            );

            instance.Construct(_projectileConfig, moveInput, _timerSystem);

            return instance;
        }
    }
}
