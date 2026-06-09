using Forma.Runtime.Movement;
using UnityEngine;

namespace Forma.Runtime.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] CharacterMovement _movement;
        
        IMoveInput _moveInput;
        
        public void Construct(IMoveInput moveInput)
        {
            _moveInput = moveInput;
        }

        void Update()
        {
            _movement.Move(_moveInput.MoveDirection);
        }
    }
}
