using UnityEngine;

namespace Forma.Runtime.Turret.Configs
{
    [CreateAssetMenu(menuName = "Forma/Configs/Turret")]
    public class TurretConfig : ScriptableObject
    {
        [field: SerializeField] public MovementConfig Movement { get; private set; }
        [field: SerializeField] public HealthConfig Health { get; private set; }
        [field: SerializeField] public AttackConfig Attack { get; private set; }

        [field: SerializeField]
        public TurretAnimationConfig Animation { get; private set; }

        [field: Min(0f)]
        [field: SerializeField]
        public float RotationSpeed { get; private set; }
    }
}
