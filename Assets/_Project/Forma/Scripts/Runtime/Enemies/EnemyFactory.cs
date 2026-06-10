using Forma.Runtime.Common;
using Forma.Runtime.Movement;
using Forma.Runtime.Player;
using UnityEngine;

namespace Forma.Runtime.Enemies
{
    public class EnemyFactory
    {
        readonly IPlayerProvider _playerProvider;

        public EnemyFactory(IPlayerProvider playerProvider)
        {
            _playerProvider = playerProvider;
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
                _playerProvider,
                instance.transform
            );
            
            instance.Construct(moveInput);
            
            return instance;
        }
    }
}
