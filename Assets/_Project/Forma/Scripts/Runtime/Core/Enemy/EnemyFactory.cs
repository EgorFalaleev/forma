using System.Collections.Generic;
using Forma.Runtime.Core.Features.Movement;
using Forma.Runtime.Core.Player;
using Forma.Runtime.Services.Time;
using UnityEngine;

namespace Forma.Runtime.Core.Enemy
{
    public class EnemyFactory
    {
        public IReadOnlyList<IRuntimeTickable> EnemyTickables => _enemyTickables;
        
        readonly Transform _playerTransform;
        readonly ITimeService _timeService;
        readonly List<IRuntimeTickable> _enemyTickables;

        public EnemyFactory(PlayerView playerView, ITimeService timeService)
        {
            _playerTransform = playerView.transform;
            _timeService = timeService;

            _enemyTickables = new List<IRuntimeTickable>();
        }

        public MovementController Create(EnemyView enemyView)
        {
            var moveInput = new EnemyMoveInput(target: _playerTransform,
                self: enemyView.transform);

            var movementController =
                new MovementController(moveInput, enemyView, _timeService);

            _enemyTickables.Add(movementController);
            
            return movementController;
        }
    }
}