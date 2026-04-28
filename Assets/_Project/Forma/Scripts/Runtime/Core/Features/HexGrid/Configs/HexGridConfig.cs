using UnityEngine;

namespace Forma.Runtime.Core.Features.HexGrid.Configs
{
    [CreateAssetMenu(menuName = "Forma/Configs/HexGrid/Grid")]
    public class HexGridConfig : ScriptableObject
    {
        [field: Range(0, 15)]
        [field: SerializeField]
        public int Radius { get; private set; }

        [field: SerializeField] public HexTileConfig HexTileConfig { get; private set; }

        [field: SerializeField]
        public HexGridAnimationConfig GridSpawnAnimationConfig { get; private set; }

        [field: SerializeField]
        public HexGridAnimationConfig GridDespawnAnimationConfig { get; private set; }
    }
}
