using Forma.Runtime.Core.Features.Damage;
using Forma.Runtime.Core.Features.Damage.Views;
using Forma.Runtime.Core.Features.Movement;
using UnityEngine;

namespace Forma.Runtime.Core.Player.Views
{
    [RequireComponent(typeof(DamageReceiverView), typeof(CharacterController))]
    public class PlayerView : MonoBehaviour, IMovableView
    {
        public IDamageReceiver DamageReceiver => _damageReceiver;
        
        [SerializeField] CharacterController _characterController;
        [SerializeField] DamageReceiverView _damageReceiver;

        public void Move(Vector3 velocity)
        {
            _characterController.Move(velocity);
        }
    }
}
