using PrimeTween;
using UnityEngine;

namespace Forma.Runtime.Turret.Configs
{
    [CreateAssetMenu(menuName = "Forma/Configs/Turret/Animation")]
    public class TurretAnimationConfig : ScriptableObject
    {
        [field: SerializeField] public float SpawnHeight { get; private set; } = 2.0f;
        [field: SerializeField] public float Duration { get; private set; } = 1.0f;
        [field: SerializeField] public Ease Easing { get; private set; }
    }
}
