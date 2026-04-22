using UnityEngine;

namespace Forma.Runtime.Core.Features.HexGrid.Configs
{
    [CreateAssetMenu(menuName = "Forma/Configs/HexGrid/Grid")]
    public class HexGridConfig : ScriptableObject
    {
        [field: SerializeField] public Vector2Int GridSize { get; private set; }
        [field: SerializeField] public HexTileConfig HexTileConfig { get; private set; }

        [field: SerializeField]
        public HexGridAnimationConfig GridSpawnAnimationConfig { get; private set; }

        [field: SerializeField]
        public HexGridAnimationConfig GridDespawnAnimationConfig { get; private set; }
    }
}
