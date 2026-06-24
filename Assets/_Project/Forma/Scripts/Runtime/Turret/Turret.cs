using Forma.Runtime.Enemies;
using Forma.Runtime.Movement;
using Forma.Runtime.Projectiles;
using Forma.Runtime.Turret.Configs;
using PrimeTween;
using R3;
using UnityEngine;

namespace Forma.Runtime.Turret
{
    public class Turret : MonoBehaviour
    {
        public Observable<Turret> OnDied => _onDied;

        [SerializeField] Movement.Movement _movement;
        [SerializeField] TriggerZone _triggerZone;
        [SerializeField] Health.Health _health;
        [SerializeField] Attack.Attack _attack;
        [SerializeField] Transform _shootPoint;

        IMoveInput _moveInput;
        TurretConfig _turretConfig;
        TurretAnimationConfig _animationConfig;
        ProjectileFactory _projectileFactory;
        Tween _currentTween;
        Transform _currentTarget;
        CompositeDisposable _disposables = new();
        Subject<Turret> _onDied = new();
        float _timer;

        public void Construct(IMoveInput moveInput, TurretConfig turretConfig,
            ProjectileFactory projectileFactory)
        {
            _moveInput = moveInput;
            _turretConfig = turretConfig;
            _animationConfig = turretConfig.Animation;
            _projectileFactory = projectileFactory;

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
        }

        void Update()
        {
            _movement.Move(_moveInput.MoveDirection);

            if (_currentTarget == null)
                return;

            TrackTarget();
            TryFireAtTarget();
        }

        void OnDestroy()
            => _disposables.Dispose();

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
            }
        }

        void OnTargetExited(Transform target)
        {
            if (target == _currentTarget)
            {
                _currentTarget = null;
                StartIdleRotation();
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
            _timer += Time.deltaTime;

            if (_timer < _turretConfig.ShootDelaySeconds)
                return;

            Vector3 directionToTarget =
                (_currentTarget.transform.position - transform.position).normalized;

            if (Vector3.Dot(transform.forward, directionToTarget) < 0.9f)
                return;

            _timer = 0f;

            _projectileFactory.Create(
                _shootPoint.position,
                _currentTarget.transform.position
            );
        }
    }
}
