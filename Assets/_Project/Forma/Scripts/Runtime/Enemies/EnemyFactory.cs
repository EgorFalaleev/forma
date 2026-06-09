using Forma.Runtime.Common;
using Forma.Runtime.Core.Common;
using Forma.Runtime.Movement;
using UnityEngine;

namespace Forma.Runtime.Enemies
{
    public class EnemyFactory
    {
        readonly ITargetProvider _targetProvider;

        public EnemyFactory(ITargetProvider targetProvider)
        {
            _targetProvider = targetProvider;
        }
        
        public Enemy Create(Vector3 position, Transform parent)
        {
            var resource = Resources.Load<Enemy>(Constants.Resources.Enemy);

            Enemy instance = Object.Instantiate(
                resource,
                position,
                Quaternion.identity,
                parent
            );

            var moveInput = new TargetFollowMoveInput(
                _targetProvider,
                instance.transform
            );
            
            instance.Construct(moveInput);
            
            return instance;
        }
    }
}
