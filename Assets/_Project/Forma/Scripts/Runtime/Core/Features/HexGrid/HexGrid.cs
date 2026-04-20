using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
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
        bool _isGridAnimating;

        public HexGrid(HexGridView hexGridView, HexGridBuilder builder,
            IToggleGridInput toggleGridInput, ITargetProvider targetProvider)
        {
            _hexGridView = hexGridView;
            _builder = builder;
            _toggleGridInput = toggleGridInput;
            _targetProvider = targetProvider;

            _toggleGridInput.OnGridModeToggled += OnToggleGrid;
        }

        void OnToggleGrid()
        {
            ToggleGrid().Forget();
        }

        async UniTaskVoid ToggleGrid()
        {
            if (_isGridAnimating)
            {
                return;
            }

            _isGridAnimating = true;
            
            if (!_isGridActive)
            {
                IEnumerable<HexTileData> gridPositions =
                    _builder.CalculateHexGrid(_targetProvider.Target.position);

                await _hexGridView.SpawnGrid(gridPositions);
            }
            else
            {
                await _hexGridView.DespawnGrid();
            }

            _isGridActive = !_isGridActive;
            _isGridAnimating = false;
        }

        public void Dispose()
        {
            _toggleGridInput.OnGridModeToggled -= OnToggleGrid;
        }
    }
}
