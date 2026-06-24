using R3;
using UnityEngine.InputSystem;

namespace Forma.Runtime.Input
{
    public class ToggleGridInputHandler : BaseInputHandler
    {
        public Observable<Unit> OnGridToggled => _onGridToggled;

        readonly InputAction _toggleGridInputAction;
        readonly Subject<Unit> _onGridToggled = new();

        public ToggleGridInputHandler(InputActions inputActions)
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

        void OnToggleGridInputActionPerformed(InputAction.CallbackContext _)
            => _onGridToggled.OnNext(Unit.Default);
    }
}
