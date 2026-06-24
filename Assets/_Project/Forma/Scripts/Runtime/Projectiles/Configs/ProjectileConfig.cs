using Forma.Runtime.Attack.Configs;
using Forma.Runtime.Movement.Configs;
using UnityEngine;

namespace Forma.Runtime.Projectiles.Configs
{
    [CreateAssetMenu(menuName = "Forma/Configs/Projectile")]
    public class ProjectileConfig : ScriptableObject
    {
        [field: Min(0f)]
        [field: SerializeField]
        public float LifetimeSeconds { get; private set; }

        [field: SerializeField] public MovementConfig Movement { get; private set; }
        [field: SerializeField] public AttackConfig Attack { get; private set; }
    }
}
