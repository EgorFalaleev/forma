using UnityEngine;

[CreateAssetMenu(menuName = "Forma/Configs/Enemy/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    [field: SerializeField] public MovementConfig Movement { get; private set; }
}