using UnityEngine;
using System.Collections.Generic;
using Forma.Runtime.Core.Features.HexGrid.Data;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public class HexGridView : MonoBehaviour
    {
        Dictionary<Vector2Int, HexView> _tiles;
        HexGridAnimator _animator;

        public void Initialize(HexGridAnimator animator,
            Dictionary<Vector2Int, HexView> tiles)
        {
            _tiles = tiles;
            _animator = animator;
        }

        public void SpawnGrid(IEnumerable<HexTileData> tileDatas)
        {
            foreach (HexTileData tile in tileDatas)
            {
                _tiles[tile.Coordinates].transform.position = tile.Position;
            }

            StartCoroutine(_animator.PlaySpawn(_tiles));
        }

        public void DespawnGrid()
        {
            StartCoroutine(_animator.PlayDespawn(_tiles));
        }
    }
}
