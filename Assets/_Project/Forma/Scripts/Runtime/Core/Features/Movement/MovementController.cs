using Forma.Runtime.Core.Features.Movement.Configs;
using Forma.Runtime.Input;
using Forma.Runtime.Movement;
using UnityEngine;

namespace Forma.Runtime.Core.Features.Movement
{
    public class MovementController : IMovementController
    {
        readonly IMoveInput _moveInput;
        readonly IMovableView _movableView;
        readonly ITimeService _timeService;
        readonly MovementConfig _movementConfig;

        public MovementController(IMoveInput moveInput, IMovableView movableView,
            ITimeService timeService, MovementConfig movementConfig)
        {
            _moveInput = moveInput;
            _movableView = movableView;
            _timeService = timeService;
            _movementConfig = movementConfig;
        }

        public void Move()
        {
            Vector3 velocity = _moveInput.MoveDirection
              * _movementConfig.Speed
              * _timeService.DeltaTime;

            _movableView.Move(velocity);
        }
    }
}
