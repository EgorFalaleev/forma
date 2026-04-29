using Forma.Runtime.Core.Features.HexGrid.Data;
using UnityEngine;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public class HexSelectionProvider
        : IHexSelectionProvider,
          IHexSelectionSetter
    {
        public Vector3? SelectedHexPosition => _selectedHexPosition;
        public HexCubeCoordinates? SelectedHexCoordinates => _selectedHexCoordinates;

        Vector3? _selectedHexPosition;
        HexCubeCoordinates? _selectedHexCoordinates;

        public void SetSelectedHexPosition(Vector3? position,
            HexCubeCoordinates? coordinates)
        {
            _selectedHexPosition = position;
            _selectedHexCoordinates = coordinates;
        }
    }
}
