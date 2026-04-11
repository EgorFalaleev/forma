using Forma.Runtime.Common;
using TMPro;
using UnityEngine;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public class HexGrid : MonoBehaviour
    {
        [SerializeField] int _width = 6;
        [SerializeField] int _height = 6;
        [SerializeField] HexCell _cellPrefab;
        [SerializeField] TextMeshProUGUI _cellLabelPrefab;
        [SerializeField] Canvas _gridCanvas;
        [SerializeField] HexMesh _hexMesh;

        HexCell[] _cells;
        Vector3 _centerOffset;

        void Awake()
        {
            _cells = new HexCell[_width * _height];

            int halfHeight = _height / 2;
            int zMin = -halfHeight;

            int zMax = _height % 2 == 0
                ? halfHeight
                : halfHeight + 1;

            int halfWidth = _width / 2;
            int xMin = -halfWidth;

            int xMax = _width % 2 == 0
                ? halfWidth
                : halfWidth + 1;

            for (int z = zMin,
                i = 0; z < zMax; z++)
            {
                for (int x = xMin; x < xMax; x++)
                {
                    CreateCell(x, z, i++);
                }
            }
        }

        void Start()
        {
            _hexMesh.Triangulate(_cells);
        }

        void CreateCell(int x, int z, int i)
        {
            var position = new Vector3
            {
                x = (x + z * 0.5f - z / 2) * (Constants.HexMetrics.InnerRadius * 2f),
                y = 0f,
                z = z * (Constants.HexMetrics.OuterRadius * 1.5f)
            };

            HexCell cell = _cells[i] = Instantiate(_cellPrefab, transform, false);
            cell.transform.localPosition = position;
            cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);

            TextMeshProUGUI label = Instantiate(_cellLabelPrefab, _gridCanvas.transform, false);
            label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
            label.text = cell.coordinates.ToStringOnSeparateLines();
        }
    }
}
