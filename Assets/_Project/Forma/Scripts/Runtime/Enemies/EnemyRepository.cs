using System.Collections.Generic;

namespace Forma.Runtime.Enemies
{
    public class EnemyRepository
    {
        readonly List<Enemy> _enemies = new();

        public void Register(Enemy enemy)
            => _enemies.Add(enemy);

        public void Unregister(Enemy enemy)
            => _enemies.Remove(enemy);
    }
}
