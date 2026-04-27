using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Forma.Runtime.Core.Features.HexGrid.Configs;
using Forma.Runtime.Core.Features.HexGrid.Data;
using Forma.Runtime.Core.Features.HexGrid.Views;
using PrimeTween;
using UnityEngine;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public class HexGridAnimator
    {
        readonly HexGridConfig _hexGridConfig;
        readonly List<KeyValuePair<int, List<HexCubeCoord>>> _ringsOrderedAsc;
        readonly List<KeyValuePair<int, List<HexCubeCoord>>> _ringsOrderedDesc;

        public HexGridAnimator(HexGridConfig hexGridConfig,
            IEnumerable<HexCubeCoord> coords, HexCubeCoord centerCoord)
        {
            _hexGridConfig = hexGridConfig;

            Dictionary<int, List<HexCubeCoord>> rings = GroupByRing(coords, centerCoord);

            _ringsOrderedAsc = rings
               .OrderBy(r => r.Key)
               .ToList();

            _ringsOrderedDesc = rings
               .OrderByDescending(r => r.Key)
               .ToList();
        }

        public async UniTask PlaySpawn(IReadOnlyDictionary<HexCubeCoord, HexView> tiles)
        {
            foreach (KeyValuePair<int, List<HexCubeCoord>> ring in _ringsOrderedAsc)
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

        public async UniTask PlayDespawn(IReadOnlyDictionary<HexCubeCoord, HexView> tiles)
        {
            foreach (KeyValuePair<int, List<HexCubeCoord>> ring in _ringsOrderedDesc)
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

        static Dictionary<int, List<HexCubeCoord>> GroupByRing(
            IEnumerable<HexCubeCoord> coords, HexCubeCoord center)
        {
            var rings = new Dictionary<int, List<HexCubeCoord>>();

            foreach (HexCubeCoord coord in coords)
            {
                int ring = HexCubeCoord.Distance(coord, center);

                if (!rings.ContainsKey(ring))
                    rings[ring] = new List<HexCubeCoord>();

                rings[ring].Add(coord);
            }

            return rings;
        }

        void AnimateRingSpawn(List<HexCubeCoord> ringCoords,
            IReadOnlyDictionary<HexCubeCoord, HexView> tiles)
        {
            foreach (HexCubeCoord coord in ringCoords)
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

        void AnimateRingDespawn(List<HexCubeCoord> tileCoords,
            IReadOnlyDictionary<HexCubeCoord, HexView> tiles)
        {
            foreach (HexCubeCoord coord in tileCoords)
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
