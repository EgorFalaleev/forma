using UnityEngine;

namespace Forma.Runtime.Core.Features.Damage.Configs
{
    [CreateAssetMenu(menuName = "Forma/Configs/Damage")]
    public class DamageConfig : ScriptableObject
    {
        [field: SerializeField] public int Amount { get; private set; }
    }
}