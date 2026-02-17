using System;
using Forma.Runtime.Core.Features.Movement;
using UnityEngine;

namespace Forma.Runtime.Core.Player
{
    public class PlayerView : MonoBehaviour, IMovableView
    {
        public event Action<Collider> OnPlayerCollided; 
        
        [SerializeField] CharacterController _characterController;

        void OnCollisionEnter(Collision other)
        {
            OnPlayerCollided?.Invoke(other.collider);
        }

        public void Move(Vector3 velocity)
        {
            _characterController.Move(velocity);
        }
    }
}