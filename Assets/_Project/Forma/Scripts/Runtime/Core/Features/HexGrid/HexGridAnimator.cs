using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Forma.Runtime.Core.Features.HexGrid.Configs;
using Forma.Runtime.Core.Features.HexGrid.Views;
using PrimeTween;
using UnityEngine;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public class HexGridAnimator
    {
        readonly HexGridConfig _hexGridConfig;
        readonly List<KeyValuePair<int, List<Vector2Int>>> _ringsOrderedAsc;
        readonly List<KeyValuePair<int, List<Vector2Int>>> _ringsOrderedDesc;

        public HexGridAnimator(HexGridConfig hexGridConfig)
        {
            _hexGridConfig = hexGridConfig;

            Dictionary<int, List<Vector2Int>> rings =
                CreateGridRings(_hexGridConfig.GridSize / 2);

            _ringsOrderedAsc = rings
               .OrderBy(r => r.Key)
               .ToList();

            _ringsOrderedDesc = rings
               .OrderByDescending(r => r.Key)
               .ToList();
        }

        public async UniTask PlaySpawn(IReadOnlyDictionary<Vector2Int, HexView> tiles)
        {
            foreach (KeyValuePair<int, List<Vector2Int>> ring in _ringsOrderedAsc)
            {
                AnimateRingSpawn(ring.Value, tiles);

                await UniTask.WaitForSeconds(
                    _hexGridConfig.GridSpawnAnimationConfig.DelayBetweenRings
                );
            }

            await UniTask.WaitForSeconds(
                _hexGridConfig.GridSpawnAnimationConfig.TileDuration
            );
        }

        public async UniTask PlayDespawn(IReadOnlyDictionary<Vector2Int, HexView> tiles)
        {
            foreach (KeyValuePair<int, List<Vector2Int>> ring in _ringsOrderedDesc)
            {
                AnimateRingDespawn(ring.Value, tiles);

                await UniTask.WaitForSeconds(
                    _hexGridConfig.GridDespawnAnimationConfig.DelayBetweenRings
                );
            }

            await UniTask.WaitForSeconds(
                _hexGridConfig.GridDespawnAnimationConfig.TileDuration
            );
        }

        Dictionary<int, List<Vector2Int>> CreateGridRings(Vector2Int centerHex)
        {
            var rings = new Dictionary<int, List<Vector2Int>>();

            Vector2Int gridSize = _hexGridConfig.GridSize;

            for (var y = 0; y < gridSize.y; y++)
            {
                for (var x = 0; x < gridSize.x; x++)
                {
                    var hexCoord = new Vector2Int(x, y);

                    int ring = Mathf.RoundToInt(Vector2Int.Distance(hexCoord, centerHex));

                    if (!rings.ContainsKey(ring))
                    {
                        rings[ring] = new List<Vector2Int>();
                    }

                    rings[ring]
                       .Add(hexCoord);
                }
            }

            return rings;
        }

        void AnimateRingSpawn(List<Vector2Int> ringCoords,
            IReadOnlyDictionary<Vector2Int, HexView> tiles)
        {
            foreach (Vector2Int coord in ringCoords)
            {
                HexView tile = tiles[coord];

                tile.gameObject.SetActive(true);

                Transform tileTransform = tile.transform;
                Vector3 targetPos = tileTransform.position;

                Vector3 startPos = targetPos
                  + Vector3.up * _hexGridConfig.GridSpawnAnimationConfig.DropHeight;

                tileTransform.position = startPos;

                Tween.Position(
                    tileTransform,
                    new TweenSettings<Vector3>(
                        targetPos,
                        _hexGridConfig.GridSpawnAnimationConfig.TileDuration,
                        _hexGridConfig.GridSpawnAnimationConfig.Easing
                    )
                );
            }
        }

        void AnimateRingDespawn(List<Vector2Int> tileCoords,
            IReadOnlyDictionary<Vector2Int, HexView> tiles)
        {
            foreach (Vector2Int coord in tileCoords)
            {
                HexView tile = tiles[coord];

                Transform tileTransform = tile.transform;
                Vector3 startPos = tileTransform.position;

                Vector3 targetPos = startPos
                  + Vector3.up * _hexGridConfig.GridDespawnAnimationConfig.DropHeight;

                Tween
                   .Position(
                        tileTransform,
                        new TweenSettings<Vector3>(
                            targetPos,
                            _hexGridConfig.GridDespawnAnimationConfig.TileDuration,
                            _hexGridConfig.GridDespawnAnimationConfig.Easing
                        )
                    )
                   .OnComplete(() => tile.gameObject.SetActive(false));
            }
        }
    }
}
