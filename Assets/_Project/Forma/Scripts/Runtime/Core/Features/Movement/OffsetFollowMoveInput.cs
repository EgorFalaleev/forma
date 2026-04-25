using Forma.Runtime.Core.Common;
using UnityEngine;

namespace Forma.Runtime.Core.Features.Movement
{
    public class OffsetFollowMoveInput : IMoveInput
    {
        const float SqrDistanceThreshold = 0.01f;

        public Vector3 MoveDirection => CalculateMoveDirection();

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

        Vector3 CalculateMoveDirection()
        {
            Vector3 targetToOrigin =
                _targetProvider.Target.position + _offset - _origin.position;

            return targetToOrigin.sqrMagnitude < SqrDistanceThreshold
                ? Vector3.zero
                : targetToOrigin.normalized;
        }
    }
}
