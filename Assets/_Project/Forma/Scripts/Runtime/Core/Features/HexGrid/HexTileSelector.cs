using System;
using Cysharp.Threading.Tasks;
using Forma.Runtime.Core.Features.HexGrid.Views;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public class HexTileSelector
    {
        public event Action<HexView> OnHexSelected; 
        public event Action OnHexDeselected; 
        
        readonly HexTileAnimator _animator;

        HexView _selectedHex;
        bool _isTileAnimating;

        public HexTileSelector(HexTileAnimator animator)
        {
            _animator = animator;
        }

        public async UniTask ClickHexTile(HexView hexView)
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

            if (_selectedHex != null)
            {
                await DeselectTile();
            }

            await SelectTile(hexView);
        }

        public void Cleanup()
        {
            if (_selectedHex != null)
            {
                _animator.Reset(_selectedHex);
                _selectedHex = null;
                
                OnHexDeselected?.Invoke();
            }
        }

        async UniTask SelectTile(HexView hexView)
        {
            _isTileAnimating = true;
            
            _selectedHex = hexView;
            OnHexSelected?.Invoke(_selectedHex);
            
            await _animator.SelectTile(hexView);

            _isTileAnimating = false;
        }

        async UniTask DeselectTile()
        {
            _isTileAnimating = true;

            await _animator.DeselectTile(_selectedHex);

            _selectedHex = null;
            OnHexDeselected?.Invoke();
            
            _isTileAnimating = false;
        }
    }
}
