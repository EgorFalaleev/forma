using Forma.Runtime.Core.Common;
using Forma.Runtime.Input;
using Forma.Runtime.Movement;
using UnityEngine;

namespace Forma.Runtime.Core.Features.Movement
{
    public class TargetFollowMoveInput : IMoveInput
    {
        public Vector3 MoveDirection
            => (_targetProvider.Target.position - _origin.position).normalized;

        readonly ITargetProvider _targetProvider;
        readonly Transform _origin;

        public TargetFollowMoveInput(ITargetProvider targetProvider, Transform origin)
        {
            _targetProvider = targetProvider;
            _origin = origin;
        }
    }
}
