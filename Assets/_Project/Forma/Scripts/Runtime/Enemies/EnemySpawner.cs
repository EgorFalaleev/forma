using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Forma.Runtime.Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Forma.Runtime.Enemies
{
    public class EnemySpawner
    {
        readonly EnemyFactory _enemyFactory;
        readonly EnemyRepository _enemyRepository;
        readonly IPlayerProvider _playerProvider;
        readonly Transform _parent;

        public EnemySpawner(EnemyFactory enemyFactory, EnemyRepository enemyRepository,
        IPlayerProvider playerProvider)
        {
            _enemyFactory = enemyFactory;
            _enemyRepository = enemyRepository;
            _playerProvider = playerProvider;
            _parent = new GameObject("Enemies").transform;
        }

        public async UniTask SpawnWaveWithDelay(int amount, float delaySeconds, CancellationToken cancellationToken)
        {
            int spawned = 0;

            while(spawned < amount)
            {
                await SpawnWithDelay(delaySeconds, cancellationToken).SuppressCancellationThrow();

                spawned++;
            }
        }

        async UniTask SpawnWithDelay(float delaySeconds, CancellationToken cancellationToken)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delaySeconds), cancellationToken: cancellationToken);

            SpawnAtRandomPosition();  
        }

        void SpawnAtRandomPosition()
        {
            var position = GetRandomPosition(_playerProvider.Transform.position, 5f, 10f);
            
            Enemy enemy = _enemyFactory.Create(position, _parent);
            _enemyRepository.Register(enemy);            
        }

        Vector3 GetRandomPosition(Vector3 center, float minRadius, float maxRadius)
        {
            var radius = Random.Range(minRadius, maxRadius);
            var randomDirection2D = Random.insideUnitCircle.normalized;
            var randomDirection = new Vector3(randomDirection2D.x, 0f, randomDirection2D.y);

            var position = center + randomDirection * radius;
            position.y = 1f;

            return position;
        }
    }
}