using UnityEngine;
using UnityEngine.InputSystem;

namespace Forma.Runtime.Services.Input
{
    public class MoveInputService : BaseInputService, IMoveInput
    {
        public Vector3 MoveDirection => _moveDirection;

        readonly InputAction _moveInputAction;
        Vector3 _moveDirection;

        public MoveInputService(InputActions inputActions) : base(inputActions)
        {
            _moveInputAction = InputActions.Player.Move;
        }

        public override void Enable()
        {
            _moveInputAction.Enable();

            _moveInputAction.performed += OnMoveInputActionPerformed;
            _moveInputAction.canceled += OnMoveInputActionCanceled;
        }

        public override void Disable()
        {
            _moveInputAction.Disable();

            _moveInputAction.performed -= OnMoveInputActionPerformed;
            _moveInputAction.canceled -= OnMoveInputActionCanceled;
        }

        void OnMoveInputActionPerformed(InputAction.CallbackContext context)
        {
            Vector2 direction = context.ReadValue<Vector2>().normalized;
            _moveDirection = new Vector3(direction.x, 0f, direction.y);
        }

        void OnMoveInputActionCanceled(InputAction.CallbackContext context)
        {
            _moveDirection = Vector3.zero;
        }
    }
}