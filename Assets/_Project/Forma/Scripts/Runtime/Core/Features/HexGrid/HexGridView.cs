using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Forma.Runtime.Core.Features.HexGrid.Data;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public class HexGridView : MonoBehaviour
    {
        Dictionary<Vector2Int, HexView> _tiles;
        HexGridAnimator _animator;
        Camera _camera;

        public void Initialize(HexGridAnimator animator,
            Dictionary<Vector2Int, HexView> tiles, Camera camera)
        {
            _tiles = tiles;
            _animator = animator;
            _camera = camera;
        }

        public async UniTask SpawnGrid(IEnumerable<HexTileData> tileDatas)
        {
            foreach (HexTileData tile in tileDatas)
            {
                _tiles[tile.Coordinates].transform.position = tile.Position;
            }

            await _animator.PlaySpawn(_tiles);
        }

        public async UniTask DespawnGrid()
        {
            await _animator.PlayDespawn(_tiles);
        }

        public bool TrySelectHexTileAt(Vector2 screenPosition, out HexView hexView)
        {
            hexView = null;
            
            Ray ray = _camera.ScreenPointToRay(screenPosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
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
