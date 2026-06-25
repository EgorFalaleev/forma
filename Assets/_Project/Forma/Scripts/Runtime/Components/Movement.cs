using Forma.Runtime.Components.Configs;
using UnityEngine;

namespace Forma.Runtime.Components
{
    public class Movement : MonoBehaviour
    {
        float _speed;

        public void Construct(MovementConfig movementConfig)
        {
            _speed = movementConfig.Speed;
        }

        public void Move(Vector3 direction)
        {
            Vector3 moveDelta = _speed * Time.deltaTime * direction;
            transform.Translate(moveDelta, Space.World);
        }
    }
}
