using System;
using Forma.Runtime.Services.Input;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public class HexGrid : IDisposable
    {
        readonly HexGridLayout _hexGridLayout;
        readonly IToggleGridInput _toggleGridInput;

        bool _isGridActive;
        
        public HexGrid(HexGridLayout hexGridLayout, IToggleGridInput toggleGridInput)
        {
            _hexGridLayout = hexGridLayout;
            _toggleGridInput = toggleGridInput;

            _toggleGridInput.OnGridModeToggled += ToggleGrid;
        }

        void ToggleGrid()
        {
            if (!_isGridActive)
            {
                _hexGridLayout.SpawnGrid();
            }
            else
            {
                _hexGridLayout.DespawnGrid();
            }

            _isGridActive = !_isGridActive;
        }

        public void Dispose()
        {
            _toggleGridInput.OnGridModeToggled -= ToggleGrid;
        }
    }
}
