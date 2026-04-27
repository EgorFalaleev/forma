using Forma.Runtime.Core.Features.Movement.Configs;
using UnityEngine;

namespace Forma.Runtime.Core.Features.Turret.Configs
{
    [CreateAssetMenu(menuName = "Forma/Configs/Turret")]
    public class TurretConfig : ScriptableObject
    {
        [field: SerializeField] public MovementConfig Movement { get; private set; }
        [field: SerializeField] public Vector3 SpawnOffset { get; private set; }

        [field: SerializeField]
        public TurretAnimationConfig Animation { get; private set; }
    }
}
