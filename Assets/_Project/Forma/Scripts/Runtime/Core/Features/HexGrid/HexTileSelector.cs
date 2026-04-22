using Cysharp.Threading.Tasks;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public class HexTileSelector
    {
        readonly HexTileAnimator _animator;

        HexView _selectedHex;
        bool _isTileAnimating;

        public HexTileSelector(HexTileAnimator animator)
        {
            _animator = animator;
        }

        public async UniTask ClickHex(HexView hexView)
        {
            if (_isTileAnimating)
            {
                return;
            }

            if (hexView == _selectedHex)
            {
                await DeselectHex();
                return;
            }

            if (_selectedHex != null)
            {
                await DeselectHex();
            }

            await SelectHex(hexView);
        }

        async UniTask SelectHex(HexView hexView)
        {
            _isTileAnimating = true;
            _selectedHex = hexView;
            
            await _animator.SelectTile(hexView);

            _isTileAnimating = false;
        }

        async UniTask DeselectHex()
        {
            _isTileAnimating = true;
            
            await _animator.DeselectTile(_selectedHex);
            
            _selectedHex = null;
            _isTileAnimating = false;
        }
    }
}
