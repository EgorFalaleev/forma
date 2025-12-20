using UnityEngine;

namespace Forma.Runtime.Core.Features.Movement
{
    public interface IMovableView
    {
        void Move(Vector3 velocity);
    }
}