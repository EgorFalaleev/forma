using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using PrimeTween;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public class HexGridLayout : MonoBehaviour
    {
        const float Sqrt3 = 1.7320508f;

        [Header("Grid settings")]
        [SerializeField] Vector2Int _gridSize;

        [Header("Tile settings")]
        [SerializeField] Material _material;
        [SerializeField] float _innerSize = 0.5f;
        [SerializeField] float _outerSize = 1f;
        [SerializeField] float _height;
        [SerializeField] bool _isFlatTopped;
        [SerializeField] bool _shouldCastShadows;
        [SerializeField] Transform _parent;
        
        [Header("Animation settings")]
        [SerializeField] float _dropHeight = 8f;
        [SerializeField] float _tileDuration = 1.5f;
        [SerializeField] float _delayBetweenRings = 0.1f;

        void OnEnable()
        {
            LayoutGrid();
        }

        [ContextMenu("Draw hex grid")]
        void LayoutGrid()
        {
            DestroyChildren(transform);

            StartCoroutine(CreateGridAnimated());
        }

        IEnumerator CreateGridAnimated()
        {
            Vector2Int centerHex = _gridSize / 2;
            Vector3 centerHexPosition = GetPositionForHexFromCoordinate(centerHex);
            Vector3 offset = _parent.position - centerHexPosition;

            Dictionary<int, List<Vector2Int>> rings = CreateGridRings(centerHex);
            
            foreach (KeyValuePair<int, List<Vector2Int>> ring in
                rings.OrderBy(r => r.Key))
            {
                AnimateRing(ring.Value, offset);

                yield return new WaitForSeconds(_delayBetweenRings);
            }
        }

        Dictionary<int, List<Vector2Int>> CreateGridRings(Vector2Int centerHex)
        {
            var rings = new Dictionary<int, List<Vector2Int>>();

            for (var y = 0; y < _gridSize.y; y++)
            {
                for (var x = 0; x < _gridSize.x; x++)
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
                Vector3 startPos = targetPos + Vector3.up * _dropHeight;

                tile.transform.position = startPos;

                Tween.Position(
                    tile.transform,
                    new TweenSettings<Vector3>(
                        targetPos,
                        _tileDuration,
                        Ease.OutBounce
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
                _material,
                _innerSize,
                _outerSize,
                _height,
                _isFlatTopped,
                _shouldCastShadows
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
            float size = _outerSize;

            if (_isFlatTopped)
            {
                shouldOffset = column % 2 == 0;
                width = 2f * size;
                height = Sqrt3 * size;

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
                width = Sqrt3 * size;
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
