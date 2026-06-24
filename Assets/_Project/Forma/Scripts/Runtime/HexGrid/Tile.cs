using System.Collections.Generic;
using System.Linq;
using Forma.Runtime.HexGrid.Configs;
using Forma.Runtime.HexGrid.Data;
using UnityEngine;
using UnityEngine.Rendering;
using VContainer;

namespace Forma.Runtime.HexGrid
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class Tile : MonoBehaviour
    {
        readonly int _baseColor = Shader.PropertyToID("_BaseColor");

        [SerializeField] MeshFilter _meshFilter;
        [SerializeField] MeshRenderer _meshRenderer;
        [SerializeField] TileAnimator _animator;

        HexTileConfig _hexTileConfig;
        Mesh _mesh;
        List<HexTileFace> _faces;
        MaterialPropertyBlock _materialPropertyBlock;

        [Inject]
        void Construct(HexGridConfig hexGridConfig)
        {
            _hexTileConfig = hexGridConfig.HexTileConfig;

            _mesh = new Mesh
            {
                name = "Hex"
            };

            _materialPropertyBlock = new MaterialPropertyBlock();

            _meshFilter.mesh = _mesh;
            _meshRenderer.sharedMaterial = _hexTileConfig.Material;

            _meshRenderer.shadowCastingMode = _hexTileConfig.ShouldCastShadows
                ? ShadowCastingMode.On
                : ShadowCastingMode.Off;

            _animator.Construct(_hexTileConfig.AnimationConfig, _materialPropertyBlock);
        }

        public void DrawMesh()
        {
            ConfigureFaces();
            CombineFaces();
        }

        public void Select()
            => _animator.PlaySelect();

        public void Unselect()
            => _animator.PlayUnselect();

        public void PrepareActive()
            => _animator.UpdateColor(_baseColor, _hexTileConfig.Material.color);

        public void PrepareInactive()
            => _animator.UpdateColor(_baseColor, _hexTileConfig.InactiveColor);

        public void UpdatePosition(Vector3 position)
            => transform.position = position;

        void ConfigureFaces()
        {
            _faces = new List<HexTileFace>();

            float innerSize = _hexTileConfig.InnerSize;
            float outerSize = _hexTileConfig.OuterSize;
            float height = _hexTileConfig.Height;

            // top faces
            AddFace(
                innerSize,
                outerSize,
                height / 2f,
                height / 2f,
                false
            );

            // bottom faces
            AddFace(
                innerSize,
                outerSize,
                -height / 2f,
                -height / 2f,
                true
            );

            // outer faces
            AddFace(
                outerSize,
                outerSize,
                height / 2f,
                -height / 2f,
                true
            );

            // inner faces
            AddFace(
                innerSize,
                innerSize,
                height / 2f,
                -height / 2f,
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

        HexTileFace CreateFace(float innerRadius, float outerRadius, float heightA,
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

            return new HexTileFace(vertices, triangles, uvs);
        }

        Vector3 GetPoint(float size, float height, int index)
        {
            float segmentDeg = 60 * index;

            float angleDeg = _hexTileConfig.IsFlatTopped
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
