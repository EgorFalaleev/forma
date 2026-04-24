using UnityEngine;

namespace Forma.Runtime.Core.Features.Movement
{
    public interface IMoveInput
    {
        Vector3 MoveDirection { get; }
    }
}