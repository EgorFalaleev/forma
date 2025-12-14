using UnityEngine;

namespace Forma.Runtime.Services.Input
{
    public interface IMoveInput
    {
        Vector3 MoveDirection { get; }
    }
}