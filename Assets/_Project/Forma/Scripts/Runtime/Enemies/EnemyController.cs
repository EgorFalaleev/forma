using UnityEngine;

namespace Forma.Runtime.Enemies
{
    public class EnemyController
    {
        readonly EnemyFactory _enemyFactory;
        readonly EnemyRepository _enemyRepository;
        readonly Transform _parent;

        public EnemyController(EnemyFactory enemyFactory, EnemyRepository enemyRepository)
        {
            _enemyFactory = enemyFactory;
            _enemyRepository = enemyRepository;
            _parent = new GameObject("Enemies").transform;
        }

        public void Spawn(Vector3 position)
        {
            Enemy enemy = _enemyFactory.Create(position, _parent);
            _enemyRepository.Register(enemy);
        }
    }
}
