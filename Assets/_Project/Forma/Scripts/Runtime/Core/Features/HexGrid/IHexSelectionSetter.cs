using Forma.Runtime.Core.Features.HexGrid.Data;
using UnityEngine;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public interface IHexSelectionSetter
    {
        void SetSelectedHexPosition(Vector3? position, HexCubeCoordinates? coordinates);
    }
}
