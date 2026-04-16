using UnityEngine;

namespace Forma.Runtime.Core.Features.HexGrid.Data
{
    [CreateAssetMenu(menuName = "Forma/Configs/HexGrid/Grid")]
    public class HexGridData : ScriptableObject
    {
        [field: SerializeField] public Vector2Int GridSize { get; private set; }
        [field: SerializeField] public HexTileData HexTileData { get; private set; }
        [field: SerializeField] public HexGridAnimationData HexGridAnimationData { get; private set; }
    }
}
