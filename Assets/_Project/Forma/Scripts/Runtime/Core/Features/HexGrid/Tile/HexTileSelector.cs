using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Forma.Runtime.Core.Features.HexGrid.Data;
using Forma.Runtime.Core.Features.HexGrid.Grid.Abstract;
using Forma.Runtime.Core.Features.HexGrid.Tile.Abstract;
using Forma.Runtime.Core.Features.HexGrid.Views;
using UnityEngine;

namespace Forma.Runtime.Core.Features.HexGrid.Tile
{
    public class HexTileSelector
        : IHexTileDeselector,
          IHexTileSelectEvents,
          IDisposable
    {
        public event Action<HexView> OnHexSelected;
        public event Action OnHexDeselected;

        readonly IHexTileClickInput _hexTileClickInput;
        readonly HexTileAnimator _hexTileAnimator;
        readonly IHexGridRegistry _hexGridRegistry;
        readonly HexTileOccupancyController _hexTileOccupancyController;

        HexView _selectedHex;
        bool _isTileAnimating;

        public HexTileSelector(IHexTileClickInput hexTileClickInput,
            HexTileAnimator hexTileAnimator, IHexGridRegistry hexGridRegistry,
            HexTileOccupancyController hexTileOccupancyController)
        {
            _hexTileClickInput = hexTileClickInput;
            _hexTileAnimator = hexTileAnimator;
            _hexGridRegistry = hexGridRegistry;
            _hexTileOccupancyController = hexTileOccupancyController;
        }

        public void Initialize()
        {
            _hexTileClickInput.OnHexClicked += OnHexTileClicked;
        }

        public void Dispose()
        {
            _hexTileClickInput.OnHexClicked -= OnHexTileClicked;
        }

        void OnHexTileClicked(HexView hexView)
        {
            ClickHexTile(hexView)
               .Forget();
        }

        async UniTask ClickHexTile(HexView hexView)
        {
            HexCubeCoordinates tileCoordinates = _hexGridRegistry.GetCoordinates(hexView);

            bool isTileActive = _hexTileOccupancyController.IsTileActive(tileCoordinates);

            if (isTileActive)
                await ProcessHexSelection(hexView);
        }

        async UniTask ProcessHexSelection(HexView hexView)
        {
            if (_isTileAnimating)
            {
                return;
            }

            if (hexView == _selectedHex)
            {
                await DeselectTile();
                return;
            }

            await DeselectTile();
            await SelectTile(hexView);
        }

        public void Cleanup()
        {
            if (_selectedHex != null)
            {
                _hexTileAnimator.Reset(_selectedHex);
                _selectedHex = null;

                OnHexDeselected?.Invoke();
            }
        }

        public async UniTask DeselectTile()
        {
            if (_selectedHex == null)
            {
                return;
            }

            _isTileAnimating = true;

            await _hexTileAnimator.DeselectTile(_selectedHex);

            _selectedHex = null;
            OnHexDeselected?.Invoke();

            _isTileAnimating = false;
        }

        async UniTask SelectTile(HexView hexView)
        {
            _isTileAnimating = true;
            
            await _hexTileAnimator.SelectTile(hexView);
            
            _selectedHex = hexView;
            OnHexSelected?.Invoke(_selectedHex);

            _isTileAnimating = false;
        }
    }
}
