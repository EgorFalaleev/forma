using UnityEngine;

namespace Forma.Runtime.Movement
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] float _speed = 5f;

        public void Move(Vector3 direction)
        {
            Vector3 moveDelta = _speed * Time.deltaTime * direction;
            transform.Translate(moveDelta, Space.World);
        }
    }
}
