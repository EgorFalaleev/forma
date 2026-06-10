using Forma.Runtime.Player;
using UnityEngine;

namespace Forma.Runtime.Movement
{
    public class TargetFollowMoveInput : IMoveInput
    {
        public Vector3 MoveDirection
            => (_playerProvider.Transform.position - _origin.position).normalized;

        readonly IPlayerProvider _playerProvider;
        readonly Transform _origin;

        public TargetFollowMoveInput(IPlayerProvider playerProvider, Transform origin)
        {
            _playerProvider = playerProvider;
            _origin = origin;
        }
    }
}
