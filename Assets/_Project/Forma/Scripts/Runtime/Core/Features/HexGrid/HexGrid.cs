using System;
using System.Collections.Generic;
using Forma.Runtime.Core.Features.HexGrid.Data;
using Forma.Runtime.Services.Input;
using Forma.Runtime.Services.TargetProvider;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public class HexGrid : IDisposable
    {
        readonly HexGridView _hexGridView;
        readonly HexGridBuilder _builder;
        readonly IToggleGridInput _toggleGridInput;
        readonly ITargetProvider _targetProvider;

        bool _isGridActive;

        public HexGrid(HexGridView hexGridView, HexGridBuilder builder,
            IToggleGridInput toggleGridInput, ITargetProvider targetProvider)
        {
            _hexGridView = hexGridView;
            _builder = builder;
            _toggleGridInput = toggleGridInput;
            _targetProvider = targetProvider;

            _toggleGridInput.OnGridModeToggled += ToggleGrid;
        }

        void ToggleGrid()
        {
            if (!_isGridActive)
            {
                IEnumerable<HexTileData> gridPositions =
                    _builder.CalculateHexGrid(_targetProvider.Target.position);

                _hexGridView.SpawnGrid(gridPositions);
            }
            else
            {
                _hexGridView.DespawnGrid();
            }

            _isGridActive = !_isGridActive;
        }

        public void Dispose()
        {
            _toggleGridInput.OnGridModeToggled -= ToggleGrid;
        }
    }
}
