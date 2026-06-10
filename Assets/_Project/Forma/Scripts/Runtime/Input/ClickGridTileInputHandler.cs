using Forma.Runtime.Camera;
using Forma.Runtime.Common;
using Forma.Runtime.HexGrid;
using R3;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Forma.Runtime.Input
{
    public class ClickGridTileInputHandler : BaseInputHandler
    {
        public Observable<Tile> OnClickedTile => _onClickedTile;

        readonly ICameraProvider _cameraProvider;
        readonly InputAction _clickTileInputAction;
        readonly Subject<Tile> _onClickedTile = new();

        public ClickGridTileInputHandler(InputActions inputActions,
            ICameraProvider cameraProvider)
        {
            _cameraProvider = cameraProvider;
            _clickTileInputAction = inputActions.Player.ClickHex;
        }

        public override void Enable()
        {
            _clickTileInputAction.Enable();

            _clickTileInputAction.performed += OnClickPerformed;
        }

        public override void Disable()
        {
            _clickTileInputAction.Disable();

            _clickTileInputAction.performed -= OnClickPerformed;
        }

        void OnClickPerformed(InputAction.CallbackContext _)
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
                if (hit.collider.TryGetComponent(out Tile clickedTile))
                {
                    _onClickedTile.OnNext(clickedTile);
                }
            }
        }
    }
}
