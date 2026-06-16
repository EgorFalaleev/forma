using UnityEngine;

[CreateAssetMenu(menuName = "Forma/Configs/Enemy/Waves")]
public class WavesConfig : ScriptableObject
{
    [field: SerializeField] public WaveData[] Waves { get; private set; }
}