using Cysharp.Threading.Tasks;
using Forma.Runtime.Core.Features.Turret.Configs;
using PrimeTween;
using UnityEngine;

namespace Forma.Runtime.Core.Features.Turret.Views
{
    public class TurretViewAnimator
    {
        readonly TurretAnimationConfig _animationConfig;

        public TurretViewAnimator(TurretConfig turretConfig)
        {
            _animationConfig = turretConfig.Animation;
        }

        public async UniTask PlaySpawnAnimation(TurretView turretView,
            Vector3 targetPosition)
        {
            await Tween.Position(
                turretView.transform,
                new TweenSettings<Vector3>(
                    targetPosition,
                    _animationConfig.Duration,
                    _animationConfig.Easing
                )
            );
        }
    }
}
