using Forma.Runtime.Core.Features.Health.Configs;
using Forma.Runtime.Core.Features.Movement.Configs;
using UnityEngine;

namespace Forma.Runtime.Core.Player.Configs
{
    [CreateAssetMenu(menuName = "Forma/Configs/Player")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public HealthConfig Health { get; private set; }
        [field: SerializeField] public MovementConfig Movement { get; private set; }
    }
}
