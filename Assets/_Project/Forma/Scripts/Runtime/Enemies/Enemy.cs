using Forma.Runtime.Movement;
using UnityEngine;

namespace Forma.Runtime.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] RigidbodyMovement _movement;

        IMoveInput _moveInput;
        
        public void Construct(IMoveInput moveInput, EnemyConfig enemyConfig)
        {
            _moveInput = moveInput;

            _movement.Construct(enemyConfig.Movement);
        }

        void FixedUpdate()
        {
            _movement.Move(_moveInput.MoveDirection);
        }
    }
}
