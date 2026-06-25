using Forma.Runtime.Components.MoveInput;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Forma.Runtime.Input
{
    public class MoveInputHandler
        : BaseInputHandler,
          IMoveInput
    {
        public Vector3 MoveDirection => _moveDirection;

        readonly InputAction _moveInputAction;
        Vector3 _moveDirection;

        public MoveInputHandler(InputActions inputActions)
        {
            _moveInputAction = inputActions.Player.Move;
        }

        public override void Enable()
        {
            _moveInputAction.Enable();

            _moveInputAction.performed += OnMoveInputPerformed;
            _moveInputAction.canceled += OnMoveInputCanceled;
        }

        public override void Disable()
        {
            _moveInputAction.Disable();

            _moveInputAction.performed -= OnMoveInputPerformed;
            _moveInputAction.canceled -= OnMoveInputCanceled;
        }

        void OnMoveInputPerformed(InputAction.CallbackContext context)
        {
            Vector2 direction = context.ReadValue<Vector2>()
               .normalized;

            _moveDirection = new Vector3(direction.x, 0f, direction.y);
        }

        void OnMoveInputCanceled(InputAction.CallbackContext _)
            => _moveDirection = Vector3.zero;
    }
}
