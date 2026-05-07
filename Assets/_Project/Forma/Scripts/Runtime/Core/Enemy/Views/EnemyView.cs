using Forma.Runtime.Core.Features.Damage;
using Forma.Runtime.Core.Features.Damage.Views;
using Forma.Runtime.Core.Features.Movement;
using UnityEngine;

namespace Forma.Runtime.Core.Enemy.Views
{
    [RequireComponent(typeof(CharacterController))]
    public class EnemyView : MonoBehaviour, IMovableView
    {
        public IDamageReceiver DamageReceiver => _damageReceiverView;
        
        [SerializeField] CharacterController _characterController;
        [SerializeField] DamageDealerView _damageDealerView;
        [SerializeField] DamageReceiverView _damageReceiverView;

        public void Initialize(int damage)
        {
            _damageDealerView.Initialize(damage);
        }
        
        public void Move(Vector3 velocity)
        {
            _characterController.Move(velocity);
        }

        public void Die()
        {
            Destroy(gameObject);
        }
    }
}