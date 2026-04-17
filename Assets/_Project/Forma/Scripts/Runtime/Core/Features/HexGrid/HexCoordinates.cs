using UnityEngine;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public class HexCoordinates : MonoBehaviour
    {
        public Vector2Int Coordinates => _coordinates;
        
        Vector2Int _coordinates;

        public void Construct(Vector2Int coordinates)
        {
            _coordinates = coordinates;
        }
    }
}
