using Forma.Runtime.Services.Input;
using Forma.Runtime.Services.TargetProvider;
using UnityEngine;

namespace Forma.Runtime.Core.Enemy
{
    public class EnemyMoveInput : IMoveInput
    {
        public Vector3 MoveDirection
            => (_targetProvider.Target.position - _self.position).normalized;

        readonly ITargetProvider _targetProvider;
        readonly Transform _self;

        public EnemyMoveInput(ITargetProvider targetProvider, Transform self)
        {
            _targetProvider = targetProvider;
            _self = self;
        }
    }
}
