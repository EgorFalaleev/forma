using UnityEngine;

namespace Forma.Runtime.Core.Features.HexGrid.Data
{
    public readonly struct HexTileData
    {
        public HexCubeCoordinates Coordinates => _coordinates;
        public Vector3 Position => _position;

        readonly HexCubeCoordinates _coordinates;
        readonly Vector3 _position;

        public HexTileData(HexCubeCoordinates coordinates, Vector3 position)
        {
            _coordinates = coordinates;
            _position = position;
        }
    }
}
