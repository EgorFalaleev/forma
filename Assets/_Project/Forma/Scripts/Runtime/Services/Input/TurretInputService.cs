using System;
using Forma.Runtime.Core.Features.Turret;
using Forma.Runtime.Core.Features.Turret.Abstract;
using UnityEngine.InputSystem;

namespace Forma.Runtime.Services.Input
{
    public class TurretInputService : BaseInputService, ITurretInput
    {
        public event Action OnPlaceTurretClicked;

        readonly InputAction _placeTurretInputAction;

        public TurretInputService(InputActions inputActions)
            : base(inputActions)
        {
            _placeTurretInputAction = inputActions.Player.PlaceTurret;
        }

        public override void Enable()
        {
            _placeTurretInputAction.Enable();

            _placeTurretInputAction.performed += OnPlaceTurretInputClicked;
        }

        public override void Disable()
        {
            _placeTurretInputAction.Disable();

            _placeTurretInputAction.performed -= OnPlaceTurretInputClicked;
        }

        void OnPlaceTurretInputClicked(InputAction.CallbackContext context)
        {
            OnPlaceTurretClicked?.Invoke();
        }
    }
}
