using System;

namespace Forma.Runtime.Core.Features.Health
{
    public interface IHealth
    {
        int Amount { get; }
        void TakeDamage(int damage);
        event Action OnDied;
    }
}