using Forma.Runtime.Core.Features.Damage.Configs;
using UnityEngine;

namespace Forma.Runtime.Core.Enemy.Configs
{
    [CreateAssetMenu(menuName = "Forma/Configs/Enemy/EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        [field: SerializeField] public DamageConfig Damage { get; private set; }
    }
}
