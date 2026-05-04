using Forma.Runtime.Core.Features.HexGrid.Data;
using UnityEngine;

namespace Forma.Runtime.Core.Features.HexGrid.Tile
{
    public interface IHexSelectionSetter
    {
        void SetSelection(Vector3? position, HexCubeCoordinates? coordinates);
        void ClearSelection();
    }
}
