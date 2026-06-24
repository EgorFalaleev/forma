using Forma.Runtime.Attack.Configs;
using Forma.Runtime.Health.Configs;
using Forma.Runtime.Movement.Configs;
using UnityEngine;

namespace Forma.Runtime.Enemies.Configs
{
    [CreateAssetMenu(menuName = "Forma/Configs/Enemy/EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        [field: SerializeField] public MovementConfig Movement { get; private set; }
        [field: SerializeField] public AttackConfig Attack { get; private set; }
        [field: SerializeField] public HealthConfig Health { get; private set; }
    }
}
