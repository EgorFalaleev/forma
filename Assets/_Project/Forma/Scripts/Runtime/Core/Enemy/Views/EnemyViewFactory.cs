using Forma.Runtime.Core.Enemy.Configs;
using UnityEngine;

namespace Forma.Runtime.Core.Enemy.Views
{
    public class EnemyViewFactory
    {
        readonly EnemyView _enemyViewPrefab;
        readonly EnemyConfig _enemyConfig;

        public EnemyViewFactory(EnemyView enemyViewPrefab, EnemyConfig enemyConfig)
        {
            _enemyViewPrefab = enemyViewPrefab;
            _enemyConfig = enemyConfig;
        }
        
        public EnemyView Create()
        {
            EnemyView enemyView = Object.Instantiate(_enemyViewPrefab);

            enemyView.Initialize(_enemyConfig.Damage.Amount);

            return enemyView;
        }
    }
}
