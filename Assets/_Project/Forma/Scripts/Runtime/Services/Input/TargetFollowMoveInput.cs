using Forma.Runtime.Services.TargetProvider;
using UnityEngine;

namespace Forma.Runtime.Services.Input
{
    public class TargetFollowMoveInput : IMoveInput
    {
        public Vector3 MoveDirection
            => (_targetProvider.Target.position - _self.position).normalized;

        readonly ITargetProvider _targetProvider;
        readonly Transform _self;

        public TargetFollowMoveInput(ITargetProvider targetProvider, Transform self)
        {
            _targetProvider = targetProvider;
            _self = self;
        }
    }
}
