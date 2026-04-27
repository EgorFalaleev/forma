using UnityEngine;

namespace Forma.Runtime.Core.Features.HexGrid.Data
{
    public readonly struct HexTileData
    {
        public HexCubeCoord Coordinates => _coordinates;
        public Vector3 Position => _position;

        readonly HexCubeCoord _coordinates;
        readonly Vector3 _position;

        public HexTileData(HexCubeCoord coordinates, Vector3 position)
        {
            _coordinates = coordinates;
            _position = position;
        }
    }
}
