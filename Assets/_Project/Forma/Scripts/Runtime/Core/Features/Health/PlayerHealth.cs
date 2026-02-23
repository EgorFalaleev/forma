using System;

namespace Forma.Runtime.Core.Features.Health
{
    public class PlayerHealth : IHealth
    {
        public event Action OnDied;

        public int Amount => _amount;

        int _amount;
        
        public PlayerHealth(int amount)
        {
            _amount = amount;
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