using Forma.Runtime.Core.Features.HexGrid.Data;
using UnityEngine;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public interface IHexSelectionProvider
    {
        Vector3? SelectedHexPosition { get; }
        HexCubeCoordinates? SelectedHexCoordinates { get; }
    }
}
