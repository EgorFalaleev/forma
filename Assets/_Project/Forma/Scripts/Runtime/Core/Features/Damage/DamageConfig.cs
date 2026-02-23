using UnityEngine;

namespace Forma.Runtime.Core.Features.Damage
{
    [CreateAssetMenu(menuName = "Configs/Damage")]
    public class DamageConfig : ScriptableObject
    {
        [field: SerializeField] public int Damage { get; private set; }
    }
}