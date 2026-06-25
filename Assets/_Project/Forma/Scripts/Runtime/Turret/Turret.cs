using Forma.Runtime.Components;
using Forma.Runtime.Components.MoveInput;
using Forma.Runtime.Enemies;
using Forma.Runtime.Projectiles;
using Forma.Runtime.Timer;
using Forma.Runtime.Turret.Configs;
using PrimeTween;
using R3;
using UnityEngine;

namespace Forma.Runtime.Turret
{
    public class Turret : MonoBehaviour
    {
        public Observable<Turret> OnDied => _onDied;

        [SerializeField] Components.Movement _movement;
        [SerializeField] TriggerZone _triggerZone;
        [SerializeField] Health _health;
        [SerializeField] Attack _attack;
        [SerializeField] Transform _shootPoint;

        readonly CompositeDisposable _disposables = new();
        readonly Subject<Turret> _onDied = new();
        IMoveInput _moveInput;
        TurretConfig _turretConfig;
        TurretAnimationConfig _animationConfig;
        ProjectileFactory _projectileFactory;
        Tween _currentTween;
        Transform _currentTarget;
        Timer.Timer _shootTimer;
        TimerSystem _timerSystem;

        public void Construct(IMoveInput moveInput, TurretConfig turretConfig,
            ProjectileFactory projectileFactory, TimerSystem timerSystem)
        {
            _moveInput = moveInput;
            _turretConfig = turretConfig;
            _animationConfig = turretConfig.Animation;
            _projectileFactory = projectileFactory;
            _timerSystem = timerSystem;

            _movement.Construct(turretConfig.Movement);
            _health.Construct(turretConfig.Health);
            _attack.Construct(turretConfig.Attack);

            _triggerZone
               .OnTransformEntered
               .Subscribe(OnTargetEntered)
               .AddTo(_disposables);

            _triggerZone
               .OnTransformExited
               .Subscribe(OnTargetExited)
               .AddTo(_disposables);

            _health
               .OnDied
               .Subscribe(Die)
               .AddTo(_disposables);

            StartShootingCooldown();
        }

        void Update()
        {
            _movement.Move(_moveInput.MoveDirection);

            if (_currentTarget == null)
                return;

            TrackTarget();
        }

        void OnDestroy()
        {
            _shootTimer?.Cancel();
            _disposables.Dispose();
        }

        void StartShootingCooldown()
        {
            _shootTimer = _timerSystem.CreateTimer(
                _turretConfig.ShootDelaySeconds,
                TryFireAtTarget
            );

            _shootTimer.Start();
        }

        void Die(Unit unit)
        {
            _onDied.OnNext(this);
            Destroy(gameObject);
        }

        void TrackTarget()
        {
            Vector3 direction = _currentTarget.position - transform.position;

            direction.y = 0f;

            if (direction == Vector3.zero)
                return;

            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                _turretConfig.RotationSpeed * Time.deltaTime
            );
        }

        void OnTargetEntered(Transform target)
        {
            if (_currentTarget == null
             && target.TryGetComponent(out Enemy _))
            {
                CancelAnimation();
                _currentTarget = target;

                _shootTimer.Start();
            }
        }

        void OnTargetExited(Transform target)
        {
            if (target == _currentTarget)
            {
                _currentTarget = null;
                StartIdleRotation();

                _shootTimer.Pause();
            }
        }

        public void PlaySpawnAnimation()
        {
            if (_currentTween.isAlive)
                return;

            Vector3 initialPosition = transform.position;

            transform.position += Vector3.up * _animationConfig.SpawnHeight;

            var targetPosition = new Vector3(
                initialPosition.x,
                transform.position.y - _animationConfig.SpawnHeight,
                initialPosition.z
            );

            gameObject.SetActive(true);

            _currentTween = Tween
               .Position(
                    transform,
                    new TweenSettings<Vector3>(
                        targetPosition,
                        _animationConfig.Duration,
                        _animationConfig.Easing
                    )
                )
               .OnComplete(StartIdleRotation);
        }

        public void CancelAnimation()
        {
            if (_currentTween.isAlive)
                _currentTween.Stop();
        }

        void StartIdleRotation()
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

        void TryFireAtTarget()
        {
            _shootTimer = null;
            
            StartShootingCooldown();

            if (_currentTarget == null)
                return;
            
            Vector3 directionToTarget =
                (_currentTarget.transform.position - transform.position).normalized;

            if (Vector3.Dot(transform.forward, directionToTarget) < 0.9f)
                return;

            _projectileFactory.Create(
                _shootPoint.position,
                _currentTarget.transform.position
            );
        }
    }
}
