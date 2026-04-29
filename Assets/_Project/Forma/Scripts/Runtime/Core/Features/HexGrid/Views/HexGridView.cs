using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Forma.Runtime.Core.Features.HexGrid.Data;
using UnityEngine;

namespace Forma.Runtime.Core.Features.HexGrid.Views
{
    public class HexGridView : MonoBehaviour
    {
        HexGridAnimator _animator;
        Camera _camera;

        public void Initialize(HexGridAnimator animator, Camera camera)
        {
            _animator = animator;
            _camera = camera;
        }

        public async UniTask SpawnGrid(IReadOnlyDictionary<HexCubeCoordinates, HexView> tiles)
        {
            await _animator.PlaySpawn(tiles);
        }

        public async UniTask DespawnGrid(IReadOnlyDictionary<HexCubeCoordinates, HexView> tiles)
        {
            await _animator.PlayDespawn(tiles);
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
