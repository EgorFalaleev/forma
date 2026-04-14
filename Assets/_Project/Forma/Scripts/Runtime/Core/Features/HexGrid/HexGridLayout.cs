using UnityEngine;

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
        [SerializeField] Transform _parent;
        
        void OnEnable()
        {
            LayoutGrid();
        }

        [ContextMenu("Draw hex grid")]
        void LayoutGrid()
        {
            DestroyChildren(transform);

            CreateGrid();
        }

        void CreateGrid()
        {
            Vector2Int centerHex = _gridSize / 2;
            Vector3 centerHexPosition = GetPositionForHexFromCoordinate(centerHex);
            Vector3 offset = _parent.position - centerHexPosition;
            
            for (int y = 0; y < _gridSize.y; y++)
            {
                for (int x = 0; x < _gridSize.x; x++)
                {
                    CreateTile(x, y, offset);
                }
            }
        }

        void CreateTile(int x, int y, Vector3 offset)
        {
            var tile = new GameObject($"Hex {x} {y}", typeof(HexRenderer));

            tile.transform.position =
                GetPositionForHexFromCoordinate(new Vector2Int(x, y)) + offset;

            var hexRenderer = tile.GetComponent<HexRenderer>();

            hexRenderer.isFlatTopped = _isFlatTopped;
            hexRenderer.outerSize = _outerSize;
            hexRenderer.innerSize = _innerSize;
            hexRenderer.height = _height;
            hexRenderer.SetMaterial(_material);
            hexRenderer.DrawMesh();

            tile.transform.SetParent(transform, true);
        }

        void DestroyChildren(Transform parent)
        {
            for (int i = 0; i < parent.childCount; i++)
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
