using Forma.Runtime.Core.Features.Movement;
using UnityEngine;

namespace Forma.Runtime.Core.Features.Turret
{
    public class TurretView : MonoBehaviour, IMovableView
    {
        public void Move(Vector3 velocity)
        {
            transform.Translate(velocity, Space.World);
        }
    }
}
