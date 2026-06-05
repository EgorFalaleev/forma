using Forma.Runtime.HexGrid.Configs;
using PrimeTween;
using UnityEngine;

namespace Forma.Runtime.HexGrid
{
    [RequireComponent(typeof(MeshRenderer))]
    public class TileAnimator : MonoBehaviour
    {
        readonly int _emissionColor = Shader.PropertyToID("_EmissionColor");

        [SerializeField] MeshRenderer _meshRenderer;
        
        HexTileAnimationConfig _hexTileAnimationConfig;
        MaterialPropertyBlock _materialPropertyBlock;
        float _initialPositionY;
        float SelectedPositionY => _initialPositionY + _hexTileAnimationConfig.Height;

        public void Construct(HexTileAnimationConfig hexTileAnimationConfig,
            MaterialPropertyBlock materialPropertyBlock)
        {
            _hexTileAnimationConfig = hexTileAnimationConfig;
            _materialPropertyBlock = materialPropertyBlock;
        }

        public void PlaySelect()
        {
            _initialPositionY = transform.position.y;

            AnimateTile(
                SelectedPositionY,
                Color.black,
                _hexTileAnimationConfig.EmissionColor
            );
        }

        public void PlayUnselect()
        {
            AnimateTile(
                _initialPositionY,
                _hexTileAnimationConfig.EmissionColor,
                Color.black
            );
        }

        public void UpdateColor(int propertyId, Color color)
        {
            _materialPropertyBlock.SetColor(propertyId, color);
            _meshRenderer.SetPropertyBlock(_materialPropertyBlock);
        }

        void AnimateTile(float positionY, Color startColor, Color endColor)
        {
            Tween.PositionY(
                transform,
                new TweenSettings<float>(
                    positionY,
                    _hexTileAnimationConfig.Duration,
                    _hexTileAnimationConfig.PositionEasing
                )
            );

            Tween.Custom(
                startColor,
                endColor,
                _hexTileAnimationConfig.Duration,
                UpdateEmissionColor,
                _hexTileAnimationConfig.ColorEasing
            );
        }

        void UpdateEmissionColor(Color color)
        {
            UpdateColor(_emissionColor, color);
        }
    }
}
