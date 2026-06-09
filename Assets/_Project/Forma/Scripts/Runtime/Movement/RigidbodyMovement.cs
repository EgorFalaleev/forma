using UnityEngine;

namespace Forma.Runtime.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyMovement : MonoBehaviour
    {
        [SerializeField] Rigidbody _rigidbody;
        [SerializeField] float _speed = 5f;

        public void Move(Vector3 direction)
        {
            Vector3 delta = _speed * Time.deltaTime * direction;
            
            _rigidbody.MovePosition(_rigidbody.position + delta);
        }
    }
}
