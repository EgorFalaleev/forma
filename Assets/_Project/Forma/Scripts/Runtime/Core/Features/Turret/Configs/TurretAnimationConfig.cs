using PrimeTween;
using UnityEngine;

namespace Forma.Runtime.Core.Features.Turret.Configs
{
    [CreateAssetMenu(menuName = "Forma/Configs/Turret/Animation")]
    public class TurretAnimationConfig : ScriptableObject
    {
        [field: SerializeField] public float Duration { get; private set; } = 1.0f;
        [field: SerializeField] public Ease Easing { get; private set; }
    }
}
