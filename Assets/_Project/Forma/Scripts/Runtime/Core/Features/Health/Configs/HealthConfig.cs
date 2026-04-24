using UnityEngine;

namespace Forma.Runtime.Core.Features.Health.Configs
{
    [CreateAssetMenu(menuName = "Forma/Configs/Health")]
    public class HealthConfig : ScriptableObject
    {
        [field: Min(0)]
        [field: SerializeField]
        public int MaxHealth { get; private set; }
    }
}
