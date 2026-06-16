using UnityEngine;

[CreateAssetMenu(menuName = "Forma/Configs/Movement")]
public class MovementConfig : ScriptableObject
{
    [field: SerializeField] public float Speed { get; private set; }
}