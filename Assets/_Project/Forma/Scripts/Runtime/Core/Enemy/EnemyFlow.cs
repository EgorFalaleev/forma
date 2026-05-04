using System.Collections.Generic;
using Forma.Runtime.Core.Enemy.Views;

namespace Forma.Runtime.Core.Enemy
{
    public class EnemyFlow
    {
        readonly EnemyFactory _enemyFactory;
        readonly EnemyViewFactory _enemyViewFactory;
        readonly List<Enemy> _enemies;

        public EnemyFlow(EnemyFactory enemyFactory, EnemyViewFactory enemyViewFactory)
        {
            _enemyFactory = enemyFactory;
            _enemyViewFactory = enemyViewFactory;

            _enemies = new List<Enemy>();
        }

        public void Initialize()
        {
            EnemyView enemyView = _enemyViewFactory.Create();
            
            Enemy enemy = _enemyFactory.Create(enemyView);

            _enemies.Add(enemy);
        }

        public void Tick()
        {
            foreach (Enemy enemy in _enemies)
            {
                enemy.Tick();
            }
        }
    }
}
