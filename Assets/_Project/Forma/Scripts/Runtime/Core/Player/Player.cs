using System;
using Forma.Runtime.Core.Features.Health;
using Forma.Runtime.Core.Features.Movement;
using Forma.Runtime.Services.Collisions;
using Forma.Runtime.Services.Collisions.Types;

namespace Forma.Runtime.Core.Player
{
    public class Player : IDisposable
    {
        readonly IMovementController _movementController;
        readonly IHealth _health;
        readonly ICollisionTriggers _collisionTriggers;

        public Player(IMovementController movementController, IHealth health,
            ICollisionTriggers collisionTriggers)
        {
            _movementController = movementController;
            _health = health;
            _collisionTriggers = collisionTriggers;

            _collisionTriggers.OnCollision += OnPlayerCollided;
        }

        void OnPlayerCollided(ICollision collision)
        {
            switch (collision)
            {
                case DamageCollision damageCollision:
                    TakeDamage(damageCollision.Damage);
                    break;
            }
        }

        public void Tick()
        {
            _movementController.Move();
        }

        public void Dispose()
        {
            _collisionTriggers.OnCollision -= OnPlayerCollided;
            _collisionTriggers.Dispose();
        }

        void TakeDamage(int damage)
        {
            _health.TakeDamage(damage);
        }
    }
}