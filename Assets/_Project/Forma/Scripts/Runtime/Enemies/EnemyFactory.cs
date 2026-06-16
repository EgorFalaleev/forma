using Forma.Runtime.Common;
using Forma.Runtime.Movement;
using Forma.Runtime.Player;
using UnityEngine;

namespace Forma.Runtime.Enemies
{
    public class EnemyFactory
    {
        readonly IPlayerProvider _playerProvider;
        readonly EnemyConfig _enemyConfig;

        public EnemyFactory(IPlayerProvider playerProvider, EnemyConfig enemyConfig)
        {
            _playerProvider = playerProvider;
            _enemyConfig = enemyConfig;
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
            
            instance.Construct(moveInput, _enemyConfig);
            
            return instance;
        }
    }
}
