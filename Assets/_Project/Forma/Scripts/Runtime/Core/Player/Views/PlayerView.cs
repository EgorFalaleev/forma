using Forma.Runtime.Core.Features.Damage;
using Forma.Runtime.Core.Features.Damage.Views;
using Forma.Runtime.Core.Features.Movement;
using UnityEngine;

namespace Forma.Runtime.Core.Player.Views
{
    [RequireComponent(typeof(DamageReceiverView), typeof(CharacterController))]
    public class PlayerView : MonoBehaviour, IMovableView
    {
        public IDamageReceiver DamageReceiver => _damageReceiverView;
        
        [SerializeField] CharacterController _characterController;
        [SerializeField] DamageReceiverView _damageReceiverView;
        [SerializeField] DamageDealerView _damageDealerView;

        public void Initialize(int damage)
        {
            _damageDealerView.Initialize(damage);
        }
        
        public void Move(Vector3 velocity)
        {
            _characterController.Move(velocity);
        }
    }
}
