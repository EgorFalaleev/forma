using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Forma.Runtime.HexGrid.Configs;
using Forma.Runtime.HexGrid.Data;
using PrimeTween;
using UnityEngine;

namespace Forma.Runtime.HexGrid
{
    public class GridAnimator
    {
        readonly GridRepository _gridRepository;
        readonly HexGridConfig _hexGridConfig;

        List<KeyValuePair<int, List<HexCubeCoordinates>>> _ringsOrderedAsc;
        List<KeyValuePair<int, List<HexCubeCoordinates>>> _ringsOrderedDesc;

        public GridAnimator(GridRepository gridRepository, HexGridConfig hexGridConfig)
        {
            _gridRepository = gridRepository;
            _hexGridConfig = hexGridConfig;
        }

        public void WarmUp()
        {
            Dictionary<int, List<HexCubeCoordinates>> rings = GroupByRing(
                _gridRepository.Tiles.Keys,
                _gridRepository.GridCenter
            );

            _ringsOrderedAsc = rings
               .OrderBy(r => r.Key)
               .ToList();

            _ringsOrderedDesc = rings
               .OrderByDescending(r => r.Key)
               .ToList();
        }

        public async UniTask PlaySpawn()
        {
            foreach ((int _, List<HexCubeCoordinates> ringTilesCoordinates) in
                _ringsOrderedAsc)
            {
                AnimateRingSpawn(ringTilesCoordinates);

                await UniTask.WaitForSeconds(
                    _hexGridConfig.GridSpawnAnimationConfig.DelayBetweenRings
                );
            }

            await UniTask.WaitForSeconds(
                _hexGridConfig.GridSpawnAnimationConfig.TileDuration
            );
        }

        public async UniTask PlayDespawn()
        {
            foreach ((int _, List<HexCubeCoordinates> ringTilesCoordinates) in
                _ringsOrderedDesc)
            {
                AnimateRingDespawn(ringTilesCoordinates);

                await UniTask.WaitForSeconds(
                    _hexGridConfig.GridDespawnAnimationConfig.DelayBetweenRings
                );
            }

            await UniTask.WaitForSeconds(
                _hexGridConfig.GridDespawnAnimationConfig.TileDuration
            );
        }

        Dictionary<int, List<HexCubeCoordinates>> GroupByRing(
            IEnumerable<HexCubeCoordinates> coords, HexCubeCoordinates center)
        {
            var rings = new Dictionary<int, List<HexCubeCoordinates>>();

            foreach (HexCubeCoordinates coord in coords)
            {
                int ring = HexCubeCoordinates.Distance(coord, center);

                if (!rings.TryGetValue(ring, out List<HexCubeCoordinates> coordinates))
                    rings[ring] = coordinates = new List<HexCubeCoordinates>();

                coordinates.Add(coord);
            }

            return rings;
        }

        void AnimateRingSpawn(List<HexCubeCoordinates> ringCoords)
        {
            foreach (HexCubeCoordinates coord in ringCoords)
            {
                Tile tile = _gridRepository.GetView(coord);

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

        void AnimateRingDespawn(List<HexCubeCoordinates> tileCoords)
        {
            foreach (HexCubeCoordinates coord in tileCoords)
            {
                Tile tile = _gridRepository.GetView(coord);

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
