using UnityEngine;

namespace Forma.Runtime.Enemies.Configs
{
    [CreateAssetMenu(menuName = "Forma/Configs/Enemy/Waves")]
    public class WavesConfig : ScriptableObject
    {
        [field: SerializeField] public WaveData[] Waves { get; private set; }
        [field: SerializeField] public float MinRadius { get; private set; }
        [field: SerializeField] public float MaxRadius { get; private set; }
        
        void OnValidate()
        {
            if (MinRadius > MaxRadius)
            {
                Debug.LogWarning(
                    "Min radius should be <= than Max radius"
                );
            }
        }
    }
}
