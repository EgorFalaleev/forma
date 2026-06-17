using Forma.Runtime.Common;
using Forma.Runtime.Projectiles.Configs;
using UnityEngine;

public class ProjectileFactory
{
    readonly ProjectileConfig _projectileConfig;
    readonly Transform _parent;

    public ProjectileFactory(ProjectileConfig projectileConfig)
    {
        _projectileConfig = projectileConfig;

        _parent = new GameObject("Projectiles").transform;
    }

    public Projectile Create(Vector3 position, Vector3 targetPosition)
    {
        var resource = Resources.Load<Projectile>(Constants.Resources.Projectile);

        var direction = (targetPosition - position).normalized;

        var moveInput = new ConstantDirectionMoveInput(direction);

        var rotation = Quaternion.LookRotation(direction);

        Projectile instance = Object.Instantiate(resource, position, rotation, _parent);

        instance.Construct(_projectileConfig, moveInput);

        return instance;
    }
}