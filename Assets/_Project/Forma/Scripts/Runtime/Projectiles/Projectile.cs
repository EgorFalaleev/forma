using Forma.Runtime.Movement;
using Forma.Runtime.Projectiles.Configs;
using Forma.Runtime.Timer;
using R3;
using UnityEngine;

namespace Forma.Runtime.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] Movement.Movement _movement;
        [SerializeField] Attack.Attack _attack;

        readonly CompositeDisposable _disposables = new();
        IMoveInput _moveInput;
        Timer.Timer _lifetimeTimer;

        public void Construct(ProjectileConfig projectileConfig, IMoveInput moveInput,
            TimerSystem timerSystem)
        {
            _moveInput = moveInput;

            _movement.Construct(projectileConfig.Movement);
            _attack.Construct(projectileConfig.Attack);

            _attack
               .OnHit
               .Subscribe(OnHit)
               .AddTo(_disposables);

            _lifetimeTimer = timerSystem.CreateTimer(
                projectileConfig.LifetimeSeconds,
                DestroySelf
            );
            
            _lifetimeTimer.Start();
        }

        void Update()
            => _movement.Move(_moveInput.MoveDirection);

        void OnDestroy()
        {
            _lifetimeTimer?.Cancel();
            _disposables.Dispose();
        }

        void OnHit(Unit _)
            => DestroySelf();

        void DestroySelf()
            => Destroy(gameObject);
    }
}
