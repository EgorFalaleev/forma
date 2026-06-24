using Forma.Runtime.Movement.Configs;
using UnityEngine;

namespace Forma.Runtime.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyMovement : MonoBehaviour
    {
        [SerializeField] Rigidbody _rigidbody;

        float _speed;

        public void Construct(MovementConfig movementConfig)
        {
            _speed = movementConfig.Speed;
        }

        public void Move(Vector3 direction)
        {
            Vector3 velocity = direction * _speed;
            _rigidbody.linearVelocity = velocity;
        }
    }
}
