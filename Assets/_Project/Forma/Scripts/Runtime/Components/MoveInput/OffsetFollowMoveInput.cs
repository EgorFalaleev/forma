using Forma.Runtime.Player;
using UnityEngine;

namespace Forma.Runtime.Components.MoveInput
{
    public class OffsetFollowMoveInput : IMoveInput
    {
        const float SqrDistanceThreshold = 0.01f;

        public Vector3 MoveDirection => CalculateMoveDirection();

        readonly IPlayerProvider _playerProvider;
        readonly Transform _origin;
        readonly Vector3 _offset;

        public OffsetFollowMoveInput(IPlayerProvider playerProvider, Transform origin,
            Vector3 offset)
        {
            _playerProvider = playerProvider;
            _origin = origin;
            _offset = offset;
        }

        Vector3 CalculateMoveDirection()
        {
            Vector3 targetToOrigin = _playerProvider.Transform.position
              + _offset
              - _origin.position;

            return targetToOrigin.sqrMagnitude < SqrDistanceThreshold
                ? Vector3.zero
                : targetToOrigin.normalized;
        }
    }
}
