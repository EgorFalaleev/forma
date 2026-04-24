using UnityEngine;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public class HexSelectionProvider : IHexSelectionProvider, IHexSelectionSetter
    {
        public Vector3? SelectedHexPosition => _selectedHexPosition;

        Vector3? _selectedHexPosition;
        
        public void SetSelectedHexPosition(Vector3? position)
        {
            _selectedHexPosition = position;
        }
    }
}
