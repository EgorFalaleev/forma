using System;
using Forma.Runtime.Common;
using Forma.Runtime.Core.Common;
using Forma.Runtime.Core.Features.HexGrid;
using Forma.Runtime.Core.Features.HexGrid.Views;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Forma.Runtime.Services.Input
{
    public class HexClickInputService
        : BaseInputService,
          IHexClickInput
    {
        public event Action<HexView> OnHexClicked;

        readonly InputAction _clickInputAction;
        readonly ICameraProvider _cameraProvider;

        public HexClickInputService(InputActions inputActions,
            ICameraProvider cameraProvider)
            : base(inputActions)
        {
            _cameraProvider = cameraProvider;
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

            Ray ray = _cameraProvider.Camera.ScreenPointToRay(screenPosition);

            if (Physics.Raycast(
                ray,
                out RaycastHit hit,
                Mathf.Infinity,
                1 << Constants.Layers.HexGrid
            ))
            {
                if (hit.collider.TryGetComponent(out HexView collidedHexView))
                {
                    OnHexClicked?.Invoke(collidedHexView);
                }
            }
        }

        public override void Disable()
        {
            _clickInputAction.Disable();

            _clickInputAction.performed -= OnClickPerformed;
        }
    }
}
