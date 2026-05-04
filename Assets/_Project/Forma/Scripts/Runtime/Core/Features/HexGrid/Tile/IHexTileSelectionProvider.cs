using Forma.Runtime.Core.Features.HexGrid.Data;
using UnityEngine;

namespace Forma.Runtime.Core.Features.HexGrid.Tile
{
    public interface IHexTileSelectionProvider
    {
        Vector3? SelectedPosition { get; }
        HexCubeCoordinates? SelectedCoordinates { get; }
    }
}
