using UnityEngine;

namespace Forma.Runtime.Movement.Configs
{
    [CreateAssetMenu(menuName = "Forma/Configs/Movement")]
    public class MovementConfig : ScriptableObject
    {
        [field: Min(0f)]
        [field: SerializeField]
        public float Speed { get; private set; }
    }
}
