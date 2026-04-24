using System.Collections.Generic;

namespace Forma.Runtime.Core.Enemy
{
    public class EnemyFlow
    {
        readonly EnemyFactory _enemyFactory;
        readonly List<Enemy> _enemies;

        public EnemyFlow(EnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;

            _enemies = new List<Enemy>();
        }

        public void Initialize()
        {
            Enemy enemy = _enemyFactory.Create();

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
