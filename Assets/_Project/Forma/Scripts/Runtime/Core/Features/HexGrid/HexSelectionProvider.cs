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

        public void SetSelection(Vector3? position,
            HexCubeCoordinates? coordinates)
        {
            _selectedHexPosition = position;
            _selectedHexCoordinates = coordinates;
        }

        public void ClearSelection()
        {
            _selectedHexPosition = null;
            _selectedHexCoordinates = null;
        }
    }
}
