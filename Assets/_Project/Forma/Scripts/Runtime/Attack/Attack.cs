using Forma.Runtime.Attack.Configs;
using R3;
using UnityEngine;

namespace Forma.Runtime.Attack
{
    [RequireComponent(typeof(Collider))]
    public class Attack : MonoBehaviour
    {
        public Observable<Unit> OnHit => _onHit;

        readonly Subject<Unit> _onHit = new();
        AttackConfig _attackConfig;

        public void Construct(AttackConfig attackConfig)
        {
            _attackConfig = attackConfig;
        }

        void OnCollisionEnter(Collision collision)
            => TryDealDamage(collision.gameObject);

        void OnTriggerEnter(Collider other)
            => TryDealDamage(other.gameObject);

        void TryDealDamage(GameObject target)
        {
            if (!IsValidTargetLayer(target.layer))
                return;

            if (target.TryGetComponent(out Health.Health health))
            {
                health.TakeDamage(_attackConfig.Damage);
                _onHit.OnNext(Unit.Default);
            }
        }

        bool IsValidTargetLayer(int targetLayer)
            => (_attackConfig.TargetLayerMask & (1 << targetLayer)) != 0;
    }
}
