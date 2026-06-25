using Forma.Runtime.Components.Configs;
using R3;
using UnityEngine;

namespace Forma.Runtime.Components
{
    public class Health : MonoBehaviour
    {
        public Observable<Unit> OnDied => _onDied;

        readonly ReactiveProperty<int> _current = new();
        readonly Subject<Unit> _onDied = new();

        public void Construct(HealthConfig healthConfig)
        {
            _current.Value = healthConfig.MaxHealth;
        }

        public void TakeDamage(int amount)
        {
            _current.Value = Mathf.Max(_current.Value - amount, 0);

            if (_current.Value == 0)
                _onDied.OnNext(Unit.Default);
        }
    }
}
