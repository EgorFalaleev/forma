using R3;
using UnityEngine;

public class Health : MonoBehaviour
{
    public Observable<Unit> OnDied => _onDied;

    ReactiveProperty<int> _current = new();
    Subject<Unit> _onDied = new();

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