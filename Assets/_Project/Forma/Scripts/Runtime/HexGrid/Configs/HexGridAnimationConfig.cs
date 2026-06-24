using PrimeTween;
using UnityEngine;

namespace Forma.Runtime.HexGrid.Configs
{
    [CreateAssetMenu(menuName = "Forma/Configs/HexGrid/Animation")]
    public class HexGridAnimationConfig : ScriptableObject
    {
        [field: SerializeField] public float DropHeight { get; private set; } = 8f;
        [field: SerializeField] public float TileDuration { get; private set; } = 1.5f;

        [field: SerializeField]
        public float DelayBetweenRings { get; private set; } = 0.1f;

        [field: SerializeField] public Ease Easing { get; private set; }
    }
}
