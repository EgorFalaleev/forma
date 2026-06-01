using UnityEngine;

namespace Forma.Runtime.Movement
{
    public interface IMoveInput
    {
        Vector3 MoveDirection { get; }
    }
}