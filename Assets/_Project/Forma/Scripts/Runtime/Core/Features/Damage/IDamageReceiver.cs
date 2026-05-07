using System;

namespace Forma.Runtime.Core.Features.Damage
{
    public interface IDamageReceiver
    {
        event Action<int> OnDamageReceived;
        void Die();
    }
}
