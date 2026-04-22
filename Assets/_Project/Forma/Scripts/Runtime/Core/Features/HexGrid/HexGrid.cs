using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Forma.Runtime.Common;
using Forma.Runtime.Core.Features.HexGrid.Data;
using Forma.Runtime.Core.Features.HexGrid.Views;
using Forma.Runtime.Services.Input;
using Forma.Runtime.Services.TargetProvider;
using UnityEngine;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public class HexGrid : IDisposable
    {
        readonly HexGridView _hexGridView;
        readonly HexGridBuilder _builder;
        readonly HexTileSelector _hexTileSelector;
        readonly IToggleGridInput _toggleGridInput;
        readonly ITargetProvider _targetProvider;
        readonly IHexClickInput _hexClickInput;

        bool _isGridActive;
        bool _isGridAnimating;

        public HexGrid(HexGridView hexGridView, HexGridBuilder builder,
            HexTileSelector hexTileSelector, IToggleGridInput toggleGridInput,
            ITargetProvider targetProvider, IHexClickInput hexClickInput)
        {
            _hexGridView = hexGridView;
            _builder = builder;
            _hexTileSelector = hexTileSelector;
            _toggleGridInput = toggleGridInput;
            _targetProvider = targetProvider;
            _hexClickInput = hexClickInput;
        }

        public void Subscribe()
        {
            _toggleGridInput.OnGridModeToggled += OnToggleGrid;
            _hexClickInput.OnClicked += OnHexTileClicked;
        }

        public void Dispose()
        {
            _toggleGridInput.OnGridModeToggled -= OnToggleGrid;
            _hexClickInput.OnClicked -= OnHexTileClicked;
        }

        void OnToggleGrid()
        {
            ToggleGrid()
               .Forget();
        }

        void OnHexTileClicked(Vector2 screenPosition)
        {
            TryClickHexTile(screenPosition)
               .Forget();
        }

        async UniTaskVoid ToggleGrid()
        {
            if (_isGridAnimating)
            {
                return;
            }

            _isGridAnimating = true;

            _hexTileSelector.Cleanup();

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

        async UniTask TryClickHexTile(Vector2 screenPosition)
        {
            if (!_isGridActive || _isGridAnimating)
            {
                return;
            }

            if (_hexGridView.TrySelectHexTileAt(
                screenPosition,
                1 << Constants.Layers.HexGrid,
                out HexView hexView
            ))
            {
                await _hexTileSelector.ClickHexTile(hexView);
            }
        }
    }
}
