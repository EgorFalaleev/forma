using Forma.Runtime.Core.Common;
using UnityEngine;

namespace Forma.Runtime.Core.Features.Movement
{
    public class OffsetFollowMoveInput : IMoveInput
    {
        public Vector3 MoveDirection
            => (_targetProvider.Target.position + _offset - _origin.position).normalized;

        readonly ITargetProvider _targetProvider;
        readonly Transform _origin;
        readonly Vector3 _offset;

        public OffsetFollowMoveInput(ITargetProvider targetProvider, Transform origin,
            Vector3 offset)
        {
            _targetProvider = targetProvider;
            _origin = origin;
            _offset = offset;
        }
    }
}
