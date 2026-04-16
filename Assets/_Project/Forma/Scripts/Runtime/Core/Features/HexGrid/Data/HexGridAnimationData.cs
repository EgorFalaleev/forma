using PrimeTween;
using UnityEngine;

namespace Forma.Runtime.Core.Features.HexGrid.Data
{
    [CreateAssetMenu(menuName = "Forma/Configs/HexGrid/Animation")]
    public class HexGridAnimationData : ScriptableObject
    {
        [field: SerializeField] public float DropHeight { get; private set; } = 8f;
        [field: SerializeField] public float TileDuration { get; private set; } = 1.5f;

        [field: SerializeField]
        public float DelayBetweenRings { get; private set; } = 0.1f;
        
        [field: SerializeField] public Ease Easing { get; private set; }
    }
}
