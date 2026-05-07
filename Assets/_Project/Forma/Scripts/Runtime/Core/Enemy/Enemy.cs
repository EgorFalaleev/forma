using System;
using Forma.Runtime.Core.Features.Damage;
using Forma.Runtime.Core.Features.Health;
using Forma.Runtime.Core.Features.Movement;

namespace Forma.Runtime.Core.Enemy
{
    public class Enemy : IDisposable
    {
        public event Action<Enemy> OnEnemyDied;
        
        readonly IMovementController _movementController;
        readonly IHealth _health;
        readonly IDamageReceiver _damageReceiver;

        public Enemy(IMovementController movementController, IHealth health,
            IDamageReceiver damageReceiver)
        {
            _movementController = movementController;
            _health = health;
            _damageReceiver = damageReceiver;
        }

        public void Initialize()
        {
            _damageReceiver.OnDamageReceived += TakeDamage;
            _health.OnDied += OnDied;
        }

        public void Tick()
        {
            _movementController.Move();
        }

        public void Dispose()
        {
            _damageReceiver.OnDamageReceived -= TakeDamage;
            _health.OnDied -= OnDied;
        }

        void TakeDamage(int amount)
        {
            _health.TakeDamage(amount);
        }

        void OnDied()
        {
            OnEnemyDied?.Invoke(this);
        }
    }
}
