using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

namespace Forma.Runtime.Core.Features.HexGrid
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class HexView : MonoBehaviour
    {
        MeshFilter _meshFilter;
        MeshRenderer _meshRenderer;
        Material _material;
        float _innerSize = 0.5f;
        float _outerSize = 1f;
        float _height;
        bool _isFlatTopped;

        Mesh _mesh;
        List<Face> _faces;

        public void Construct(Material material, float innerSize, float outerSize,
            float height, bool isFlatTopped, bool shouldCastShadows)
        {
            _meshFilter = GetComponent<MeshFilter>();
            _meshRenderer = GetComponent<MeshRenderer>();
            
            _mesh = new Mesh
            {
                name = "Hex"
            };

            _meshFilter.mesh = _mesh;
            _meshRenderer.material = material;

            _innerSize = innerSize;
            _outerSize = outerSize;
            _height = height;
            _isFlatTopped = isFlatTopped;

            _meshRenderer.shadowCastingMode = shouldCastShadows
                ? ShadowCastingMode.On
                : ShadowCastingMode.Off;
        }

        public void DrawMesh()
        {
            DrawFaces();
            CombineFaces();
        }

        void DrawFaces()
        {
            _faces = new List<Face>();

            // top faces
            AddFace(
                _innerSize,
                _outerSize,
                _height / 2f,
                _height / 2f,
                false
            );

            // bottom faces
            AddFace(
                _innerSize,
                _outerSize,
                -_height / 2f,
                -_height / 2f,
                true
            );

            // outer faces
            AddFace(
                _outerSize,
                _outerSize,
                _height / 2f,
                -_height / 2f,
                true
            );

            // inner faces
            AddFace(
                _innerSize,
                _innerSize,
                _height / 2f,
                -_height / 2f,
                false
            );
        }

        void AddFace(float innerRadius, float outerRadius, float heightA, float heightB,
            bool reverse)
        {
            for (int point = 0; point < 6; point++)
            {
                _faces.Add(
                    CreateFace(
                        innerRadius,
                        outerRadius,
                        heightA,
                        heightB,
                        point,
                        reverse
                    )
                );
            }
        }

        void CombineFaces()
        {
            var vertices = new List<Vector3>();
            var triangles = new List<int>();
            var uvs = new List<Vector2>();

            for (int i = 0; i < _faces.Count; i++)
            {
                vertices.AddRange(_faces[i].Vertices);
                uvs.AddRange(_faces[i].Uvs);

                int offset = 4 * i;

                triangles.AddRange(
                    _faces[i]
                       .Triangles
                       .Select(triangle => triangle + offset)
                );
            }

            _mesh.vertices = vertices.ToArray();
            _mesh.triangles = triangles.ToArray();
            _mesh.uv = uvs.ToArray();
            _mesh.RecalculateNormals();
        }

        Face CreateFace(float innerRadius, float outerRadius, float heightA,
            float heightB, int point, bool reverse)
        {
            Vector3 pointA = GetPoint(innerRadius, heightB, point);

            Vector3 pointB = GetPoint(
                innerRadius,
                heightB,
                point < 5
                    ? point + 1
                    : 0
            );

            Vector3 pointC = GetPoint(
                outerRadius,
                heightA,
                point < 5
                    ? point + 1
                    : 0
            );

            Vector3 pointD = GetPoint(outerRadius, heightA, point);

            var vertices = new List<Vector3>
            {
                pointA,
                pointB,
                pointC,
                pointD
            };

            var triangles = new List<int>
            {
                0,
                1,
                2,
                2,
                3,
                0
            };

            var uvs = new List<Vector2>
            {
                new(0, 0),
                new(1, 0),
                new(1, 1),
                new(0, 1)
            };

            if (reverse)
            {
                vertices.Reverse();
            }

            return new Face(vertices, triangles, uvs);
        }

        Vector3 GetPoint(float size, float height, int index)
        {
            float segmentDeg = 60 * index;

            float angleDeg = _isFlatTopped
                ? segmentDeg
                : segmentDeg - 30;

            float angleRad = angleDeg * Mathf.Deg2Rad;

            return new Vector3(
                size * Mathf.Cos(angleRad),
                height,
                size * Mathf.Sin(angleRad)
            );
        }
    }
}
