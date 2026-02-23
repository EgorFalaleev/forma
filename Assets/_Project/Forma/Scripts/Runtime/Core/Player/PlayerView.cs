using System;
using Forma.Runtime.Core.Features.Collisions;
using Forma.Runtime.Core.Features.Movement;
using UnityEngine;

namespace Forma.Runtime.Core.Player
{
    [RequireComponent(typeof(Collider), typeof(CharacterController))]
    public class PlayerView : MonoBehaviour, IMovableView, ICollidableView
    {
        public event Action<Collider> OnCollided;

        [SerializeField] CharacterController _characterController;

        void OnCollisionEnter(Collision other)
        {
            OnCollided?.Invoke(other.collider);
        }

        public void Move(Vector3 velocity)
        {
            _characterController.Move(velocity);
        }
    }
}