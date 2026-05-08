using System;
using System.Collections.Generic;
using Forma.Runtime.Core.Enemy.Abstract;
using Forma.Runtime.Core.Enemy.Views;

namespace Forma.Runtime.Core.Enemy
{
    public class EnemyFlow
        : IEnemyRegistry,
          IDisposable
    {
        public IEnumerable<Enemy> Enemies => _enemies.Keys;
        
        readonly EnemyFactory _enemyFactory;
        readonly EnemyViewFactory _enemyViewFactory;
        readonly Dictionary<Enemy, EnemyView> _enemies;
        readonly List<Enemy> _enemiesToRemove;

        public EnemyFlow(EnemyFactory enemyFactory, EnemyViewFactory enemyViewFactory)
        {
            _enemyFactory = enemyFactory;
            _enemyViewFactory = enemyViewFactory;

            _enemies = new Dictionary<Enemy, EnemyView>();
            _enemiesToRemove = new List<Enemy>();
        }

        public void Initialize()
        {
            EnemyView enemyView = _enemyViewFactory.Create();

            Enemy enemy = _enemyFactory.Create(enemyView);

            enemy.Initialize();

            enemy.OnEnemyDied += OnEnemyDied;

            _enemies.Add(enemy, enemyView);
        }

        void OnEnemyDied(Enemy enemy)
        {
            _enemiesToRemove.Add(enemy);
        }

        public void Tick()
        {
            foreach (Enemy enemy in _enemies.Keys)
            {
                enemy.Tick();
            }

            foreach (Enemy enemy in _enemiesToRemove)
            {
                _enemies[enemy]
                   .Die();

                enemy.OnEnemyDied -= OnEnemyDied;
                enemy.Dispose();
                _enemies.Remove(enemy);
            }

            _enemiesToRemove.Clear();
        }

        public void Dispose()
        {
            foreach (Enemy enemy in _enemies.Keys)
            {
                enemy.OnEnemyDied -= OnEnemyDied;
                enemy.Dispose();
            }

            _enemiesToRemove.Clear();
            _enemies.Clear();
        }
    }
}
