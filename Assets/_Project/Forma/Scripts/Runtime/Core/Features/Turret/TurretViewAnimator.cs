using Cysharp.Threading.Tasks;
using Forma.Runtime.Core.Features.Turret.Configs;
using PrimeTween;
using UnityEngine;

namespace Forma.Runtime.Core.Features.Turret
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

        public void PlayInfiniteRotation(TurretView turretView)
        {
            Tween.LocalRotation(
                turretView.transform,
                new TweenSettings<Vector3>(
                    new Vector3(0, 180, 0),
                    duration: 2f,
                    cycles: -1,
                    cycleMode: CycleMode.Incremental,
                    ease: Ease.Linear
                )
            );
        }
    }
}
