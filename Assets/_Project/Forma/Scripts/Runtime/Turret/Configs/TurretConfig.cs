using UnityEngine;

namespace Forma.Runtime.Turret.Configs
{
    [CreateAssetMenu(menuName = "Forma/Configs/Turret")]
    public class TurretConfig : ScriptableObject
    {
        [field: SerializeField]
        public TurretAnimationConfig Animation { get; private set; }

        [field: Min(0f)]
        [field: SerializeField]
        public float RotationSpeed { get; private set; }
    }
}
