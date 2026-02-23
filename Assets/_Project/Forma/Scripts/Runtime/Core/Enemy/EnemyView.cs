using Forma.Runtime.Core.Features.Damage;
using Forma.Runtime.Core.Features.Movement;
using UnityEngine;

namespace Forma.Runtime.Core.Enemy
{
    [RequireComponent(typeof(CharacterController))]
    public class EnemyView : MonoBehaviour, IMovableView, IDamageDealerView
    {
        [SerializeField] CharacterController _characterController;
        [SerializeField] DamageConfig _damageConfig;

        public DamageConfig DamageConfig => _damageConfig;

        public void Move(Vector3 velocity)
        {
            _characterController.Move(velocity);
        }
    }
}