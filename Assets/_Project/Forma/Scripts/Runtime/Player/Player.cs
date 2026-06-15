using Forma.Runtime.Movement;
using UnityEngine;

namespace Forma.Runtime.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] RigidbodyMovement _movement;
        [SerializeField] float _speed = 5f;

        IMoveInput _moveInput;

        public void Construct(IMoveInput moveInput)
        {
            _moveInput = moveInput;
        }

        void FixedUpdate()
        {
            var delta = _moveInput.MoveDirection * _speed;
            _movement.Move(delta);
        }

        {
        }
    }
}
