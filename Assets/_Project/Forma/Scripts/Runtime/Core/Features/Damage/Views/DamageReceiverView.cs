using System;
using UnityEngine;

namespace Forma.Runtime.Core.Features.Damage.Views
{
    [RequireComponent(typeof(Collider))]
    public class DamageReceiverView : MonoBehaviour, IDamageReceiver
    {
        public event Action<int> OnDamageReceived;

        void OnCollisionEnter(Collision other)
        {
            if (other.collider.TryGetComponent(out DamageDealerView damageDealerView))
            {
                OnDamageReceived?.Invoke(damageDealerView.Damage);
            }
        }
    }
}
