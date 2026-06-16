using UnityEngine;

[CreateAssetMenu(menuName = "Forma/Configs/Player")]
public class PlayerConfig : ScriptableObject
{
    [field: SerializeField] public HealthConfig Health { get; private set; }
    [field: SerializeField] public MovementConfig Movement { get; private set; }
}