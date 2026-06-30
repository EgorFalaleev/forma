using System.Collections.Generic;
using R3;

namespace Forma.Runtime.Enemies
{
    public class EnemyRepository
    {
        readonly List<Enemy> _enemies = new();
        readonly CompositeDisposable _disposables = new();

        public void Register(Enemy enemy)
        {
            _enemies.Add(enemy);

            enemy
               .Health
               .OnDied
               .Subscribe(_ => Unregister(enemy))
               .AddTo(_disposables);
        }

        void Unregister(Enemy enemy)
            => _enemies.Remove(enemy);
    }
}
