using System.Collections.Generic;
using Forma.Runtime.Core.Features.Movement;

namespace Forma.Runtime.Core.Enemy
{
    public class EnemyFlow
    {
        readonly EnemyFactory _enemyFactory;
        readonly IEnumerable<EnemyView> _enemyViews;
        readonly List<IMovementController> _movementControllers;

        public EnemyFlow(EnemyFactory enemyFactory, IEnumerable<EnemyView> enemyViews)
        {
            _enemyFactory = enemyFactory;
            _enemyViews = enemyViews;

            _movementControllers = new List<IMovementController>();
        }

        public void Initialize()
        {
            foreach (EnemyView enemyView in _enemyViews)
            {
                IMovementController movementController = _enemyFactory.Create(enemyView);

                _movementControllers.Add(movementController);
            }
        }

        public void Tick()
        {
            foreach (IMovementController movementController in _movementControllers)
            {
                movementController.Move();
            }
        }
    }
}