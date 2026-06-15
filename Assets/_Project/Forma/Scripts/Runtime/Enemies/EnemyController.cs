using System.Threading;
using Cysharp.Threading.Tasks;

namespace Forma.Runtime.Enemies
{
    public class EnemyController
    {
        readonly EnemySpawner _enemySpawner;
        CancellationTokenSource _cancellationTokenSource;

        public EnemyController(EnemySpawner enemySpawner)
        {
            _enemySpawner = enemySpawner;
        }

        public async UniTaskVoid StartSpawning()
        {
            _cancellationTokenSource = new();
            await _enemySpawner.SpawnWaveWithDelay(5, 1f, _cancellationTokenSource.Token);
        }

        public void StopSpawning()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }        
    }
}
