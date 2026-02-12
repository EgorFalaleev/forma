using System.Collections.Generic;
using Forma.Runtime.Core.Features.Movement;

namespace Forma.Runtime.Core.Enemy
{
    public class EnemyFlow
    {
        readonly EnemyFactory _enemyFactory;
        readonly IEnumerable<EnemyView> _enemyViews;
        readonly List<MovementController> _movementControllers;

        public EnemyFlow(EnemyFactory enemyFactory, IEnumerable<EnemyView> enemyViews)
        {
            _enemyFactory = enemyFactory;
            _enemyViews = enemyViews;

            _movementControllers = new List<MovementController>();
        }

        public void Initialize()
        {
            foreach (EnemyView enemyView in _enemyViews)
            {
                MovementController movementController = _enemyFactory.Create(enemyView);

                _movementControllers.Add(movementController);
            }
        }

        public void Tick()
        {
            foreach (MovementController movementController in _movementControllers)
            {
                movementController.Tick();
            }
        }
    }
}