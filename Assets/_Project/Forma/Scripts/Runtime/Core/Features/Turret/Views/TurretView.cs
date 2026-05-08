using Forma.Runtime.Core.Features.Movement;
using PrimeTween;
using UnityEngine;

namespace Forma.Runtime.Core.Features.Turret.Views
{
    public class TurretView
        : MonoBehaviour,
          IMovableView
    {
        Tween _currentTween;

        public void Move(Vector3 velocity)
        {
            transform.Translate(velocity, Space.World);
        }

        public void StartIdleRotation()
        {
            _currentTween = Tween.LocalRotation(
                transform,
                new TweenSettings<Vector3>(
                    new Vector3(0, 180, 0),
                    duration: 2f,
                    cycles: -1,
                    cycleMode: CycleMode.Incremental,
                    ease: Ease.Linear
                )
            );
        }

        public void StopIdleRotation()
        {
            _currentTween.Stop();
        }
    }
}
