using System.Threading;
using Cysharp.Threading.Tasks;

namespace Forma.Runtime.Enemies
{
    public class EnemyController
    {
        readonly EnemySpawner _enemySpawner;
        readonly WavesConfig _wavesConfig;
        CancellationTokenSource _cancellationTokenSource;

        public EnemyController(EnemySpawner enemySpawner, WavesConfig wavesConfig)
        {
            _enemySpawner = enemySpawner;
            _wavesConfig = wavesConfig;
        }

        public async UniTaskVoid StartSpawning()
        {
            _cancellationTokenSource = new();

            WaveData nextWave = _wavesConfig.Waves[0];

            await _enemySpawner.SpawnWaveWithDelay(nextWave.Amount, nextWave.SpawnDelay, _cancellationTokenSource.Token);
        }

        public void StopSpawning()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }        
    }
}
