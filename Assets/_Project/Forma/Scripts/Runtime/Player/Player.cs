using Forma.Runtime.Movement;
using R3;
using UnityEngine;

namespace Forma.Runtime.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] RigidbodyMovement _movement;
        [SerializeField] Health _health;
        [SerializeField] float _speed = 5f;

        IMoveInput _moveInput;
        CompositeDisposable _disposables = new();

        public void Construct(IMoveInput moveInput)
        {
            _moveInput = moveInput;

            _health.OnDied.Subscribe(OnDied).AddTo(_disposables);
        }

        void FixedUpdate()
        {
            var delta = _moveInput.MoveDirection * _speed;
            _movement.Move(delta);
        }

        
        void OnDied(Unit _)
        {
            Destroy(gameObject);
        }
    }
}
