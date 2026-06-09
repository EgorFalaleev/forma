using Forma.Runtime.Core.Common;
using UnityEngine;

namespace Forma.Runtime.Movement
{
    public class TargetFollowMoveInput : IMoveInput
    {
        public Vector3 MoveDirection
            => (_targetProvider.Transform.position - _origin.position).normalized;

        readonly ITargetProvider _targetProvider;
        readonly Transform _origin;

        public TargetFollowMoveInput(ITargetProvider targetProvider, Transform origin)
        {
            _targetProvider = targetProvider;
            _origin = origin;
        }
    }
}
