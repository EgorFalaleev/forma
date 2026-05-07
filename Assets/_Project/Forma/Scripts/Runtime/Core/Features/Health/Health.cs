using System;
using Forma.Runtime.Core.Features.Health.Configs;

namespace Forma.Runtime.Core.Features.Health
{
    public class Health : IHealth
    {
        public event Action OnDied;

        public int Amount => _amount;

        int _amount;
        
        public Health(HealthConfig healthConfig)
        {
            _amount = healthConfig.MaxHealth;
        }

        public void TakeDamage(int damage)
        {
            _amount -= damage;

            if (_amount <= 0)
            {
                _amount = 0;
                OnDied?.Invoke();
            }
        }
    }
}