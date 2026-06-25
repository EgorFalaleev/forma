using UnityEngine;

namespace Forma.Runtime.Components.MoveInput
{
    public interface IMoveInput
    {
        Vector3 MoveDirection { get; }
    }
}