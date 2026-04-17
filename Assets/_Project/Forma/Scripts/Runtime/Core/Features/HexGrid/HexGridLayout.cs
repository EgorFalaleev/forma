using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Forma.Runtime.Common;
using Forma.Runtime.Core.Features.HexGrid.Data;
using PrimeTween;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public class HexGridLayout : MonoBehaviour
    {
        HexGridData _hexGridData;
        Transform _parent;

        HexTileData HexTileData => _hexGridData.HexTileData;

        HexGridAnimationData GridSpawnAnimationData
            => _hexGridData.GridSpawnAnimationData;

        HexGridAnimationData GridDespawnAnimationData
            => _hexGridData.GridDespawnAnimationData;

        public void Initialize(HexGridData hexGridData, Transform parent)
        {
            _hexGridData = hexGridData;
            _parent = parent;
        }

        public void SpawnGrid()
        {
            StartCoroutine(CreateGridAnimated());
        }

        public void DespawnGrid()
        {
            StartCoroutine(DestroyGridAnimated());
        }

        IEnumerator DestroyGridAnimated()
        {
            Vector2Int centerHex = _hexGridData.GridSize / 2;

            Dictionary<int, List<Transform>> rings = GroupChildrenByRing(centerHex);

            foreach (KeyValuePair<int, List<Transform>> ring in rings.OrderByDescending(
                ring => ring.Key
            ))
            {
                AnimateRingDespawn(ring.Value);

                yield return new WaitForSeconds(GridDespawnAnimationData.DelayBetweenRings);
            }

            yield return new WaitForSeconds(GridDespawnAnimationData.TileDuration);

            DestroyChildren(transform);
        }

        void AnimateRingDespawn(List<Transform> tiles)
        {
            foreach (Transform tile in tiles)
            {
                Vector3 startPos = tile.position;

                Vector3 targetPos =
                    startPos + Vector3.up * GridDespawnAnimationData.DropHeight;

                Tween
                   .Position(
                        tile,
                        new TweenSettings<Vector3>(
                            targetPos,
                            GridDespawnAnimationData.TileDuration,
                            GridDespawnAnimationData.Easing
                        )
                    )
                   .OnComplete(() => tile.gameObject.SetActive(false));
            }
        }

        Dictionary<int, List<Transform>> GroupChildrenByRing(Vector2Int centerHex)
        {
            var rings = new Dictionary<int, List<Transform>>();

            foreach (Transform child in transform)
            {
                var tileCoords = child.GetComponent<HexCoordinates>();

                int ring = Mathf.RoundToInt(
                    Vector2Int.Distance(tileCoords.Coordinates, centerHex)
                );

                if (!rings.ContainsKey(ring))
                {
                    rings[ring] = new List<Transform>();
                }

                rings[ring]
                   .Add(child.transform);
            }

            return rings;
        }

        IEnumerator CreateGridAnimated()
        {
            Vector2Int centerHex = _hexGridData.GridSize / 2;
            Vector3 centerHexPosition = GetPositionForHexFromCoordinate(centerHex);
            Vector3 offset = _parent.position - centerHexPosition;

            Dictionary<int, List<Vector2Int>> coordRings = CreateGridRings(centerHex);

            foreach (KeyValuePair<int, List<Vector2Int>> ring in coordRings.OrderBy(
                r => r.Key
            ))
            {
                AnimateRing(ring.Value, offset);

                yield return new WaitForSeconds(GridSpawnAnimationData.DelayBetweenRings);
            }
        }

        Dictionary<int, List<Vector2Int>> CreateGridRings(Vector2Int centerHex)
        {
            var rings = new Dictionary<int, List<Vector2Int>>();

            Vector2Int gridSize = _hexGridData.GridSize;

            for (var y = 0; y < gridSize.y; y++)
            {
                for (var x = 0; x < gridSize.x; x++)
                {
                    int ring = Mathf.RoundToInt(
                        Vector2Int.Distance(new Vector2Int(x, y), centerHex)
                    );

                    if (!rings.ContainsKey(ring))
                    {
                        rings[ring] = new List<Vector2Int>();
                    }

                    rings[ring]
                       .Add(new Vector2Int(x, y));
                }
            }

            return rings;
        }

        void AnimateRing(List<Vector2Int> ringCoords, Vector3 offset)
        {
            foreach (Vector2Int coord in ringCoords)
            {
                HexRenderer tile = CreateTile(coord, offset);

                Transform tileTransform = tile.transform;
                Vector3 targetPos = tileTransform.position;

                Vector3 startPos =
                    targetPos + Vector3.up * GridSpawnAnimationData.DropHeight;

                tileTransform.position = startPos;

                Tween.Position(
                    tileTransform,
                    new TweenSettings<Vector3>(
                        targetPos,
                        GridSpawnAnimationData.TileDuration,
                        GridSpawnAnimationData.Easing
                    )
                );
            }
        }

        HexRenderer CreateTile(Vector2Int coords, Vector3 offset)
        {
            var tileGo = new GameObject($"Hex {coords.x} {coords.y}");

            tileGo.transform.position = GetPositionForHexFromCoordinate(coords) + offset;

            var hexRenderer = tileGo.AddComponent<HexRenderer>();
            var hexCoords = tileGo.AddComponent<HexCoordinates>();

            hexRenderer.Construct(
                HexTileData.Material,
                HexTileData.InnerSize,
                HexTileData.OuterSize,
                HexTileData.Height,
                HexTileData.IsFlatTopped,
                HexTileData.ShouldCastShadows
            );

            hexCoords.Construct(coords);

            hexRenderer.DrawMesh();

            tileGo.transform.SetParent(transform, true);

            return hexRenderer;
        }

        void DestroyChildren(Transform parent)
        {
            for (var i = 0; i < parent.childCount; i++)
            {
                Destroy(
                    parent.GetChild(i)
                       .gameObject
                );
            }
        }

        Vector3 GetPositionForHexFromCoordinate(Vector2Int coordinate)
        {
            int column = coordinate.x;
            int row = coordinate.y;

            float width;
            float height;
            float xPosition;
            float yPosition;
            bool shouldOffset;
            float horizontalDistance;
            float verticalDistance;
            float offset;
            float size = HexTileData.OuterSize;

            if (HexTileData.IsFlatTopped)
            {
                shouldOffset = column % 2 == 0;
                width = 2f * size;
                height = Constants.Math.Sqrt3 * size;

                horizontalDistance = width * 3f / 4f;
                verticalDistance = height;

                offset = shouldOffset
                    ? height / 2f
                    : 0;

                xPosition = column * horizontalDistance;
                yPosition = row * verticalDistance - offset;
            }
            else
            {
                shouldOffset = row % 2 == 0;
                width = Constants.Math.Sqrt3 * size;
                height = 2f * size;

                horizontalDistance = width;
                verticalDistance = height * 3f / 4f;

                offset = shouldOffset
                    ? width / 2f
                    : 0;

                xPosition = column * horizontalDistance + offset;
                yPosition = row * verticalDistance;
            }

            return new Vector3(xPosition, 0, -yPosition);
        }
    }
}
