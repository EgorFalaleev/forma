using Cysharp.Threading.Tasks;
using Forma.Runtime.Core.Features.HexGrid.Configs;
using Forma.Runtime.Core.Features.HexGrid.Views;
using PrimeTween;
using UnityEngine;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public class HexTileAnimator
    {
        readonly HexTileAnimationConfig _hexTileAnimationConfig;

        Vector3 _startPos;
        Sequence _currentSequence;

        public HexTileAnimator(HexTileAnimationConfig hexTileAnimationConfig)
        {
            _hexTileAnimationConfig = hexTileAnimationConfig;
        }

        public async UniTask SelectTile(HexView hexView)
        {
            Vector3 tilePosition = hexView.transform.position;

            _startPos = tilePosition;

            Vector3 targetPos = tilePosition
              + Vector3.up * _hexTileAnimationConfig.Height;

            _currentSequence = CreateSequence(
                hexView,
                Color.black,
                _hexTileAnimationConfig.EmissionColor,
                targetPos
            );

            await _currentSequence;
        }

        public async UniTask DeselectTile(HexView hexView)
        {
            _currentSequence = CreateSequence(
                hexView,
                _hexTileAnimationConfig.EmissionColor,
                Color.black,
                _startPos
            );

            await _currentSequence;
        }

        public void Reset(HexView hexView)
        {
            if (_currentSequence.isAlive)
            {
                _currentSequence.Stop();
            }

            hexView.UpdateEmissionColor(Color.black);
        }

        Sequence CreateSequence(HexView hexView, Color startColor, Color endColor,
            Vector3 targetPos)
        {
            Tween colorTween = Tween.Custom(
                startColor,
                endColor,
                _hexTileAnimationConfig.Duration,
                hexView.UpdateEmissionColor,
                _hexTileAnimationConfig.ColorEasing
            );

            Tween positionTween = Tween.Position(
                hexView.transform,
                new TweenSettings<Vector3>(
                    targetPos,
                    _hexTileAnimationConfig.Duration,
                    _hexTileAnimationConfig.PositionEasing
                )
            );

            return Sequence
               .Create()
               .Group(positionTween)
               .Group(colorTween);
        }
    }
}
