using Cysharp.Threading.Tasks;
using Forma.Runtime.Core.Features.HexGrid.Data;
using PrimeTween;
using UnityEngine;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public class HexTileAnimator
    {
        readonly HexTileAnimationConfig _hexTileAnimationConfig;

        Vector3 _startPos;

        public HexTileAnimator(HexTileAnimationConfig hexTileAnimationConfig)
        {
            _hexTileAnimationConfig = hexTileAnimationConfig;
        }

        public async UniTask SelectTile(HexView hexView)
        {
            Transform tileTransform = hexView.transform;
            Vector3 tilePosition = tileTransform.position;

            _startPos = tilePosition;

            Vector3 targetPos = tilePosition
              + Vector3.up * _hexTileAnimationConfig.Height;

            await Tween.Position(
                tileTransform,
                new TweenSettings<Vector3>(
                    targetPos,
                    _hexTileAnimationConfig.Duration,
                    _hexTileAnimationConfig.Easing
                )
            );
        }

        public async UniTask DeselectTile(HexView hexView)
        {
            Transform tileTransform = hexView.transform;

            await Tween.Position(
                tileTransform,
                new TweenSettings<Vector3>(
                    _startPos,
                    _hexTileAnimationConfig.Duration,
                    _hexTileAnimationConfig.Easing
                )
            );
        }
    }
}
