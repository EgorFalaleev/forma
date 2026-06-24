using UnityEngine;

namespace Forma.Runtime.Enemies.Configs
{
    [CreateAssetMenu(menuName = "Forma/Configs/Enemy/Waves")]
    public class WavesConfig : ScriptableObject
    {
        [field: SerializeField] public WaveData[] Waves { get; private set; }
    }
}
