using Forma.Runtime.Services.Input;
using Forma.Runtime.Services.Time;
using UnityEngine;
using VContainer.Unity;

namespace Forma.Runtime.Core.Features.Movement
{
    public class MovementController : ITickable
    {
        // TODO get speed from config
        const float SPEED = 10f;

        readonly IMoveInput _moveInput;
        readonly IMovableView _movableView;
        readonly ITimeService _timeService;

        public MovementController(IMoveInput moveInput, IMovableView movableView,
            ITimeService timeService)
        {
            _moveInput = moveInput;
            _movableView = movableView;
            _timeService = timeService;
        }

        public void Tick()
        {
            Vector3 velocity = _moveInput.MoveDirection * SPEED * _timeService.deltaTime;

            _movableView.Move(velocity);
        }
    }
}