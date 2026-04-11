using System.Collections.Generic;
using Forma.Runtime.Common;
using UnityEngine;

namespace Forma.Runtime.Core.Features.HexGrid
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class HexMesh : MonoBehaviour
    {
        [SerializeField] MeshFilter _meshFilter;

        Mesh _hexMesh;
        List<Vector3> _vertices;
        List<int> _triangles;

        void Awake()
        {
            _meshFilter.mesh = _hexMesh = new Mesh();
            _hexMesh.name = "Hex Mesh";

            _vertices = new List<Vector3>();
            _triangles = new List<int>();
        }

        public void Triangulate(HexCell[] cells)
        {
            _hexMesh.Clear();
            _vertices.Clear();
            _triangles.Clear();

            foreach (HexCell cell in cells)
            {
                Triangulate(cell);
            }

            _hexMesh.SetVertices(_vertices);
            _hexMesh.SetTriangles(_triangles, 0);
            _hexMesh.RecalculateNormals();
        }

        void Triangulate(HexCell cell)
        {
            for (int i = 0; i < 6; i++)
            {
                Vector3 center = cell.transform.localPosition;

                AddTriangle(
                    center,
                    center + Constants.HexMetrics.Corners[i],
                    center + Constants.HexMetrics.Corners[i + 1]
                );
            }
        }

        void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            int vertexIndex = _vertices.Count;

            _vertices.Add(v1);
            _vertices.Add(v2);
            _vertices.Add(v3);

            _triangles.Add(vertexIndex);
            _triangles.Add(vertexIndex + 1);
            _triangles.Add(vertexIndex + 2);
        }
    }
}
