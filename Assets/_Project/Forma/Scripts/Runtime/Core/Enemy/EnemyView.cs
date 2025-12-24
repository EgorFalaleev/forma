using Forma.Runtime.Core.Features.Movement;
using UnityEngine;

namespace Forma.Runtime.Core.Enemy
{
    public class EnemyView : MonoBehaviour, IMovableView
    {
        [SerializeField] CharacterController _characterController;
        
        public void Move(Vector3 velocity)
        {
            _characterController.Move(velocity);
        }
    }
}