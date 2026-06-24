using R3;
using UnityEngine.InputSystem;

namespace Forma.Runtime.Input
{
    public class PlaceTurretInputHandler : BaseInputHandler
    {
        public Observable<Unit> OnPlaceTurretClicked => _onPlaceTurretClicked;

        readonly InputAction _placeTurretInputAction;
        readonly Subject<Unit> _onPlaceTurretClicked = new();

        public PlaceTurretInputHandler(InputActions inputActions)
        {
            _placeTurretInputAction = inputActions.Player.PlaceTurret;
        }

        public override void Enable()
        {
            _placeTurretInputAction.Enable();

            _placeTurretInputAction.performed += OnPlaceTurretInputPerformed;
        }

        public override void Disable()
        {
            _placeTurretInputAction.Disable();

            _placeTurretInputAction.performed -= OnPlaceTurretInputPerformed;
        }

        void OnPlaceTurretInputPerformed(InputAction.CallbackContext _)
            => _onPlaceTurretClicked.OnNext(Unit.Default);
    }
}
