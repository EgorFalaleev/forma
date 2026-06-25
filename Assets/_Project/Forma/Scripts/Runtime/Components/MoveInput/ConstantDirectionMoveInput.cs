using UnityEngine;

namespace Forma.Runtime.Components.MoveInput
{
    public class ConstantDirectionMoveInput : IMoveInput
    {
        public Vector3 MoveDirection => _moveDirection;

        readonly Vector3 _moveDirection;

        public ConstantDirectionMoveInput(Vector3 moveDirection)
        {
            _moveDirection = moveDirection;
        }
    }
}
