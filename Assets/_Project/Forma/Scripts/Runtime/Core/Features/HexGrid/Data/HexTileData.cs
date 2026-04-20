using UnityEngine;

namespace Forma.Runtime.Core.Features.HexGrid.Data
{
    public readonly struct HexTileData
    {
        public Vector2Int Coordinates => _coordinates;
        public Vector3 Position => _position;
        
        readonly Vector2Int _coordinates;
        readonly Vector3 _position;

        public HexTileData(Vector2Int coordinates, Vector3 position)
        {
            _coordinates = coordinates;
            _position = position;
        }
    }
}
