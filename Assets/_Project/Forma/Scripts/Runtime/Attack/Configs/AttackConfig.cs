using UnityEngine;

[CreateAssetMenu(menuName = "Forma/Configs/Attack")]
public class AttackConfig : ScriptableObject
{
    [field: Min(0)] [field: SerializeField] public int Damage { get; private set; }
}