using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Attack : MonoBehaviour
{
    int _damage;

    public void Construct(AttackConfig attackConfig)
    {
        _damage = attackConfig.Damage;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Health health))
        {
            health.TakeDamage(_damage);
        }
    }
}