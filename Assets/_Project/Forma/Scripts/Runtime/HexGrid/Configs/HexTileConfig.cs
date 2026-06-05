using UnityEngine;

namespace Forma.Runtime.HexGrid.Configs
{
    [CreateAssetMenu(menuName = "Forma/Configs/HexGrid/Tile")]
    public class HexTileConfig : ScriptableObject
    {
        [field: SerializeField] public Material Material { get; private set; }
        [field: SerializeField] public float InnerSize { get; private set; } = 1.5f;
        [field: SerializeField] public float OuterSize { get; private set; } = 2f;

        [field: SerializeField]
        [field: Range(0f, 20f)]
        public float Height { get; private set; } = 0.5f;

        [field: SerializeField] public bool IsFlatTopped { get; private set; }
        [field: SerializeField] public bool ShouldCastShadows { get; private set; }
        [field: SerializeField] public HexTileAnimationConfig AnimationConfig { get; private set; }
        [field: SerializeField] public Color InactiveColor { get; private set; }

        void OnValidate()
        {
            if (InnerSize > OuterSize)
            {
                Debug.LogWarning(
                    "Inner size is greater than outer size. Mesh will be inverted"
                );
            }
        }
    }
}
