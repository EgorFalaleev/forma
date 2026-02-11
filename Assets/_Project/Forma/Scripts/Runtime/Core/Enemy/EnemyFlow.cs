namespace Forma.Runtime.Core.Enemy
{
    public class EnemyFlow
    {
        readonly EnemyFactory _enemyFactory;

        public EnemyFlow(EnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
        }

        public void Tick()
        {
            foreach (IRuntimeTickable enemyTickable in _enemyFactory.EnemyTickables)
                enemyTickable.Tick();
        }
    }
}