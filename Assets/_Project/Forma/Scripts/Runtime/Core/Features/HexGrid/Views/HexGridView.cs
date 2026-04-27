using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Forma.Runtime.Core.Features.HexGrid.Data;
using UnityEngine;

namespace Forma.Runtime.Core.Features.HexGrid.Views
{
    public class HexGridView : MonoBehaviour
    {
        Dictionary<HexCubeCoord, HexView> _tiles;
        HexGridAnimator _animator;
        Camera _camera;

        public void Initialize(HexGridAnimator animator,
            Dictionary<HexCubeCoord, HexView> tiles, Camera camera)
        {
            _tiles = tiles;
            _animator = animator;
            _camera = camera;
        }

        public void UpdatePositions(IEnumerable<HexTileData> tileDatas)
        {
            foreach (HexTileData tile in tileDatas)
                _tiles[tile.Coordinates].SetGridPosition(tile.Position);
        }

        public async UniTask SpawnGrid()
        {
            foreach (HexView tile in _tiles.Values)
                tile.transform.position = tile.GridPosition;

            await _animator.PlaySpawn(_tiles);
        }

        public async UniTask DespawnGrid()
        {
            await _animator.PlayDespawn(_tiles);
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
