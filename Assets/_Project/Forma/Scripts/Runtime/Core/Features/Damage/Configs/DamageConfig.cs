using UnityEngine;

namespace Forma.Runtime.Core.Features.Damage.Configs
{
    [CreateAssetMenu(menuName = "Forma/Configs/Damage/DamageConfig")]
    public class DamageConfig : ScriptableObject
    {
        [field: SerializeField] public int Amount { get; private set; }
    }
}