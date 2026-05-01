using UnityEngine;

namespace Forma.Runtime.Core.Features.HexGrid.Views
{
    public class HexGridView
    {
        Camera _camera;

        public void Initialize(Camera camera)
        {
            _camera = camera;
        }

        public bool TrySelectHexTileAt(Vector2 screenPosition, int layerMask,
            out HexView hexView)
        {
            hexView = null;

            Ray ray = _camera.ScreenPointToRay(screenPosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
            {
                if (hit.collider.TryGetComponent(out HexView collidedHexView))
                {
                    hexView = collidedHexView;
                    return true;
                }
            }

            return false;
        }
    }
}
