using System;
using Forma.Runtime.Services.Input;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public class HexGrid : IDisposable
    {
        readonly HexGridLayout _hexGridLayout;
        readonly IToggleGridInput _toggleGridInput;

        public HexGrid(HexGridLayout hexGridLayout, IToggleGridInput toggleGridInput)
        {
            _hexGridLayout = hexGridLayout;
            _toggleGridInput = toggleGridInput;

            _toggleGridInput.OnGridModeToggled += ToggleGrid;
        }

        void ToggleGrid()
        {
            _hexGridLayout.LayoutGrid();
        }

        public void Dispose()
        {
            _toggleGridInput.OnGridModeToggled -= ToggleGrid;
        }
    }
}
