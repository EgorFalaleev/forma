using System;
using Forma.Runtime.Core.Features.HexGrid;
using UnityEngine.InputSystem;

namespace Forma.Runtime.Services.Input
{
    public class ToggleGridInputService  : BaseInputService, IToggleGridInput
    {
        public event Action OnGridModeToggled;
        
        readonly InputAction _toggleGridInputAction;

        public ToggleGridInputService(InputActions inputActions) : base(inputActions)
        {
            _toggleGridInputAction = inputActions.Player.ToggleGrid;
        }
        
        public override void Enable()
        {
            _toggleGridInputAction.Enable();

            _toggleGridInputAction.performed += OnToggleGridInputActionPerformed;
        }

        public override void Disable()
        {
            _toggleGridInputAction.Disable();
            
            _toggleGridInputAction.performed -= OnToggleGridInputActionPerformed;
        }

        void OnToggleGridInputActionPerformed(InputAction.CallbackContext context)
        {
            OnGridModeToggled?.Invoke();
        }
    }
}