using Forma.Runtime.Movement;
using R3;
using UnityEngine;

namespace Forma.Runtime.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] RigidbodyMovement _movement;
        [SerializeField] Health _health;
        [SerializeField] Attack _attack;

        IMoveInput _moveInput;
        CompositeDisposable _disposables = new();

        public void Construct(IMoveInput moveInput, PlayerConfig playerConfig)
        {
            _moveInput = moveInput;

            _health.Construct(playerConfig.Health);
            _movement.Construct(playerConfig.Movement);
            _attack.Construct(playerConfig.Attack);

            _health.OnDied.Subscribe(OnDied).AddTo(_disposables);
        }

        void FixedUpdate()
        {
            _movement.Move(_moveInput.MoveDirection);
        }

        
        void OnDied(Unit _)
        {
            Destroy(gameObject);
        }
    }
}
