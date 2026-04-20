using System.Collections.Generic;
using UnityEngine;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public readonly struct Face
    {
        public IEnumerable<Vector3> Vertices => _vertices;
        public IEnumerable<int> Triangles => _triangles;
        public IEnumerable<Vector2> Uvs => _uvs;

        readonly List<Vector3> _vertices;
        readonly List<int> _triangles;
        readonly List<Vector2> _uvs;

        public Face(List<Vector3> vertices, List<int> triangles, List<Vector2> uvs)
        {
            _vertices = vertices;
            _triangles = triangles;
            _uvs = uvs;
        }
    }
}
