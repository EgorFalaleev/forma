using System;
using Forma.Runtime.Core.Features.HexGrid;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Forma.Runtime.Services.Input
{
    public class HexClickInputService
        : BaseInputService,
          IHexClickInput
    {
        public event Action<Vector2> OnClicked;

        readonly InputAction _clickInputAction;

        public HexClickInputService(InputActions inputActions)
            : base(inputActions)
        {
            _clickInputAction = inputActions.Player.ClickHex;
        }

        public override void Enable()
        {
            _clickInputAction.Enable();

            _clickInputAction.performed += OnClickPerformed;
        }

        void OnClickPerformed(InputAction.CallbackContext context)
        {
            Vector2 screenPosition = Mouse.current.position.ReadValue();
            
            OnClicked?.Invoke(screenPosition);
        }

        public override void Disable()
        {
            _clickInputAction.Disable();

            _clickInputAction.performed -= OnClickPerformed;
        }
    }
}
