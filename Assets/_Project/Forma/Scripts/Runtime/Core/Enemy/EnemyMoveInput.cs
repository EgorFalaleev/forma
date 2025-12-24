using Forma.Runtime.Services.Input;
using UnityEngine;

namespace Forma.Runtime.Core.Enemy
{
    public class EnemyMoveInput : IMoveInput
    {
        public Vector3 MoveDirection => (_target.position - _self.position).normalized;

        readonly Transform _target;
        readonly Transform _self;

        public EnemyMoveInput(Transform target, Transform self)
        {
            _target = target;
            _self = self;
        }
    }
}