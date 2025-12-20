using Forma.Runtime.Services.Input;
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

        public MovementController(IMoveInput moveInput, IMovableView movableView)
        {
            _moveInput = moveInput;
            _movableView = movableView;
        }

        public void Tick()
        {
            // TODO remove get deltaTime from service
            Vector3 velocity = _moveInput.MoveDirection * SPEED * Time.deltaTime;

            _movableView.Move(velocity);
        }
    }
}