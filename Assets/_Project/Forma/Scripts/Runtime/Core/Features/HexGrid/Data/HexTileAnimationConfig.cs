using PrimeTween;
using UnityEngine;

namespace Forma.Runtime.Core.Features.HexGrid.Data
{
    [CreateAssetMenu(menuName = "Forma/Configs/HexGrid/Animation/Tile")]
    public class HexTileAnimationConfig : ScriptableObject
    {
        [field: SerializeField] public float Height { get; private set; } = 4f;
        [field: SerializeField] public float Duration { get; private set; } = 0.5f;
        [field: SerializeField] public Ease Easing { get; private set; }
    }
}
