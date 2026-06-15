using R3;
using UnityEngine;

public class Health : MonoBehaviour
{
    public Observable<Unit> OnDied => _onDied;

    [SerializeField] int _max = 10;

    ReactiveProperty<int> _current = new();
    Subject<Unit> _onDied = new();

    void Start()
    {
        _current.Value = _max;
    }

    public void TakeDamage(int amount)
    {
        _current.Value = Mathf.Max(_current.Value - amount, 0);

        if (_current.Value == 0)
            _onDied.OnNext(Unit.Default);
    }
}    