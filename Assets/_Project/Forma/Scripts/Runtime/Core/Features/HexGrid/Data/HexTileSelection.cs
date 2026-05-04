using UnityEngine;

namespace Forma.Runtime.Core.Features.HexGrid.Data
{
    public struct HexTileSelection
    {
        public Vector3 WorldPosition => _worldPosition;
        public HexCubeCoordinates Coordinates => _coordinates;

        Vector3 _worldPosition;
        HexCubeCoordinates _coordinates;

        public HexTileSelection(Vector3 worldPosition, HexCubeCoordinates coordinates)
        {
            _worldPosition = worldPosition;
            _coordinates = coordinates;
        }
    }
}
