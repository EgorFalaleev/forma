using Forma.Runtime.Core.Features.Movement;
using PrimeTween;
using UnityEngine;

namespace Forma.Runtime.Core.Features.Turret.Views
{
    public class TurretView
        : MonoBehaviour,
          IMovableView,
          ITurretView
    {
        public Transform Transform => transform;

        Tween _currentTween;

        public void Move(Vector3 velocity)
        {
            transform.Translate(velocity, Space.World);
        }

        public void StartIdleRotation()
        {
            if (_currentTween.isAlive)
                return;

            Vector3 target = transform.localEulerAngles + new Vector3(0, 180f, 0);

            _currentTween = Tween.LocalRotation(
                transform,
                new TweenSettings<Vector3>(
                    target,
                    duration: 2f,
                    cycles: -1,
                    cycleMode: CycleMode.Incremental,
                    ease: Ease.Linear
                )
            );
        }

        public void StopIdleRotation()
        {
            if (_currentTween.isAlive)
                _currentTween.Stop();
        }

        public void LookAtTarget(Vector3 position, float delta)
        {
            Vector3 direction = position - transform.position;

            direction.y = 0f;

            if (direction == Vector3.zero)
                return;

            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                delta
            );
        }
    }
}
