using Forma.Runtime.Attack.Configs;
using Forma.Runtime.Health.Configs;
using Forma.Runtime.Movement.Configs;
using UnityEngine;

namespace Forma.Runtime.Player.Configs
{
    [CreateAssetMenu(menuName = "Forma/Configs/Player")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public HealthConfig Health { get; private set; }
        [field: SerializeField] public MovementConfig Movement { get; private set; }
        [field: SerializeField] public AttackConfig Attack { get; private set; }
    }
}
