using System;
using Cysharp.Threading.Tasks;
using Forma.Runtime.Core.Features.HexGrid.Data;
using Forma.Runtime.Core.Features.HexGrid.Views;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public class HexTileSelector
        : IHexTileDeselector,
          IDisposable
    {
        public event Action<HexView> OnHexSelected;
        public event Action OnHexDeselected;

        readonly IHexClickInput _hexClickInput;
        readonly HexTileAnimator _hexTileAnimator;
        readonly HexTileRegistry _hexTileRegistry;
        readonly HexOccupancyController _hexOccupancyController;
        readonly HexGridStateController _hexGridStateController;

        HexView _selectedHex;
        bool _isTileAnimating;

        public HexTileSelector(IHexClickInput hexClickInput,
            HexTileAnimator hexTileAnimator, HexTileRegistry hexTileRegistry,
            HexOccupancyController hexOccupancyController,
            HexGridStateController hexGridStateController)
        {
            _hexClickInput = hexClickInput;
            _hexTileAnimator = hexTileAnimator;
            _hexTileRegistry = hexTileRegistry;
            _hexOccupancyController = hexOccupancyController;
            _hexGridStateController = hexGridStateController;
        }

        public void Initialize()
        {
            _hexClickInput.OnHexClicked += OnHexClicked;
        }

        public void Dispose()
        {
            _hexClickInput.OnHexClicked -= OnHexClicked;
        }

        void OnHexClicked(HexView hexView)
        {
            ClickHexTile(hexView)
               .Forget();
        }

        async UniTask ClickHexTile(HexView hexView)
        {
            if (_hexGridStateController.State != HexGridState.Visible)
                return;

            HexCubeCoordinates tileCoordinates = _hexTileRegistry.GetCoordinates(hexView);

            bool isTileActive = _hexOccupancyController.IsTileActive(tileCoordinates);

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

            _selectedHex = hexView;
            OnHexSelected?.Invoke(_selectedHex);

            await _hexTileAnimator.SelectTile(hexView);

            _isTileAnimating = false;
        }
    }
}
