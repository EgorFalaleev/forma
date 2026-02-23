using System;
using Forma.Runtime.Core.Features.Collisions;
using Forma.Runtime.Core.Features.Damage;
using Forma.Runtime.Services.Collisions.Types;
using UnityEngine;

namespace Forma.Runtime.Services.Collisions
{
    public class CollisionTriggers : ICollisionTriggers
    {
        public event Action<ICollision> OnCollision;

        readonly ICollidableView _collidableView;

        public CollisionTriggers(ICollidableView collidableView)
        {
            _collidableView = collidableView;
            
            _collidableView.OnCollided += OnCollided;
        }

        public void Dispose()
        {
            _collidableView.OnCollided -= OnCollided;
        }

        void OnCollided(Collider collider)
        {
            if (collider.TryGetComponent(out IDamageDealerView damageDealerView))
            {
                int damage = damageDealerView.DamageConfig.Damage;

                OnCollision?.Invoke(new DamageCollision(damage));
            }
        }
    }
}