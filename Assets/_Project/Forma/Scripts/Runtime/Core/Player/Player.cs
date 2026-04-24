using System;
using Forma.Runtime.Core.Features.Damage;
using Forma.Runtime.Core.Features.Health;
using Forma.Runtime.Core.Features.Movement;

namespace Forma.Runtime.Core.Player
{
    public class Player : IDisposable
    {
        readonly IMovementController _movementController;
        readonly IHealth _health;
        readonly IDamageReceiver _damageReceiver;

        public Player(IMovementController movementController, IHealth health,
            IDamageReceiver damageReceiver)
        {
            _movementController = movementController;
            _health = health;
            _damageReceiver = damageReceiver;
            
            _damageReceiver.OnDamageReceived += TakeDamage;
        }

        public void Tick()
        {
            _movementController.Move();
        }

        public void Dispose()
        {
            _damageReceiver.OnDamageReceived -= TakeDamage;
        }

        void TakeDamage(int damage)
        {
            _health.TakeDamage(damage);
        }
    }
}
