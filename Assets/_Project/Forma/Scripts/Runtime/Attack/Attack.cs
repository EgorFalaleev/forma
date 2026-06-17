using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Attack : MonoBehaviour
{
    AttackConfig _attackConfig;

    public void Construct(AttackConfig attackConfig)
    {
        _attackConfig = attackConfig;
    }

    void OnCollisionEnter(Collision collision)
    {
        TryDealDamage(collision.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        TryDealDamage(other.gameObject);
    }

    void TryDealDamage(GameObject target)
    {
        if (!IsValidTargetLayer(target.layer))
            return;

        if (target.TryGetComponent(out Health health))
            health.TakeDamage(_attackConfig.Damage);
    }

    bool IsValidTargetLayer(int targetLayer)
    {
        return (_attackConfig.TargetLayerMask & (1 << targetLayer)) != 0;
    }
}