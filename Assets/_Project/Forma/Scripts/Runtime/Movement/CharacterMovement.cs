using UnityEngine;

namespace Forma.Runtime.Movement
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] CharacterController _characterController;
        [SerializeField] float _speed = 5f;

        public void Move(Vector3 direction)
        {
            Vector3 moveDelta = _speed * Time.deltaTime * direction;
            
            _characterController.Move(moveDelta);
        }
    }
}
