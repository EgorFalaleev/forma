using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Forma.Runtime.Enemies.Configs;
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
        readonly WavesConfig _wavesConfig;
        readonly Transform _parent;

        public EnemySpawner(EnemyFactory enemyFactory, EnemyRepository enemyRepository,
            IPlayerProvider playerProvider, WavesConfig wavesConfig)
        {
            _enemyFactory = enemyFactory;
            _enemyRepository = enemyRepository;
            _playerProvider = playerProvider;
            _wavesConfig = wavesConfig;
            _parent = new GameObject("Enemies").transform;
        }

        public async UniTask SpawnWaveWithDelay(int amount, float delaySeconds,
            CancellationToken cancellationToken)
        {
            var spawned = 0;

            while (spawned < amount)
            {
                await SpawnWithDelay(delaySeconds, cancellationToken)
                   .SuppressCancellationThrow();

                spawned++;
            }
        }

        async UniTask SpawnWithDelay(float delaySeconds,
            CancellationToken cancellationToken)
        {
            await UniTask.Delay(
                TimeSpan.FromSeconds(delaySeconds),
                cancellationToken: cancellationToken
            );

            SpawnAtRandomPosition();
        }

        void SpawnAtRandomPosition()
        {
            Vector3 position = GetRandomPosition(
                _playerProvider.Transform.position,
                _wavesConfig.MinRadius,
                _wavesConfig.MaxRadius
            );

            Enemy enemy = _enemyFactory.Create(position, _parent);
            _enemyRepository.Register(enemy);
        }

        Vector3 GetRandomPosition(Vector3 center, float minRadius, float maxRadius)
        {
            float radius = Random.Range(minRadius, maxRadius);
            Vector2 randomDirection2D = Random.insideUnitCircle.normalized;

            var randomDirection = new Vector3(
                randomDirection2D.x,
                0f,
                randomDirection2D.y
            );

            Vector3 position = center + randomDirection * radius;
            position.y = 1f;

            return position;
        }
    }
}
