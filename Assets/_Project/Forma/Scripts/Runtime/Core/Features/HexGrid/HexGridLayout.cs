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
        HexGridAnimationData HexGridAnimationData => _hexGridData.HexGridAnimationData;

        public void Initialize(HexGridData hexGridData, Transform parent)
        {
            _hexGridData = hexGridData;
            _parent = parent;
        }
        
        [ContextMenu("Draw hex grid")]
        public void LayoutGrid()
        {
            DestroyChildren(transform);

            StartCoroutine(CreateGridAnimated());
        }

        IEnumerator CreateGridAnimated()
        {
            Vector2Int centerHex = _hexGridData.GridSize / 2;
            Vector3 centerHexPosition = GetPositionForHexFromCoordinate(centerHex);
            Vector3 offset = _parent.position - centerHexPosition;

            Dictionary<int, List<Vector2Int>> rings = CreateGridRings(centerHex);

            foreach (KeyValuePair<int, List<Vector2Int>> ring in
                rings.OrderBy(r => r.Key))
            {
                AnimateRing(ring.Value, offset);

                yield return new WaitForSeconds(HexGridAnimationData.DelayBetweenRings);
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
                GameObject tile = CreateTile(coord.x, coord.y, offset);

                Vector3 targetPos = tile.transform.position;

                Vector3 startPos =
                    targetPos + Vector3.up * HexGridAnimationData.DropHeight;

                tile.transform.position = startPos;

                Tween.Position(
                    tile.transform,
                    new TweenSettings<Vector3>(
                        targetPos,
                        HexGridAnimationData.TileDuration,
                        HexGridAnimationData.Easing
                    )
                );
            }
        }

        GameObject CreateTile(int x, int y, Vector3 offset)
        {
            var tile = new GameObject($"Hex {x} {y}", typeof(HexRenderer));

            tile.transform.position =
                GetPositionForHexFromCoordinate(new Vector2Int(x, y)) + offset;

            var hexRenderer = tile.GetComponent<HexRenderer>();

            hexRenderer.Construct(
                HexTileData.Material,
                HexTileData.InnerSize,
                HexTileData.OuterSize,
                HexTileData.Height,
                HexTileData.IsFlatTopped,
                HexTileData.ShouldCastShadows
            );

            hexRenderer.DrawMesh();

            tile.transform.SetParent(transform, true);

            return tile;
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
