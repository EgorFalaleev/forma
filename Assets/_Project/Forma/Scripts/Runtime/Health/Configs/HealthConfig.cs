using UnityEngine;

[CreateAssetMenu(menuName = "Forma/Configs/Health")]
public class HealthConfig : ScriptableObject
{
    [field: Min(1)] [field: SerializeField] public int MaxHealth { get; private set; }
}