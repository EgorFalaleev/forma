using Forma.Runtime.Core.Features.Health.Configs;
using UnityEngine;

namespace Forma.Runtime.Core.Player.Configs
{
    [CreateAssetMenu(menuName = "Forma/Configs/Player/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public HealthConfig Health { get; private set; }
    }
}
