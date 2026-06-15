using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Attack : MonoBehaviour
{
    [SerializeField] int _damage;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Health health))
        {
            health.TakeDamage(_damage);
        }
    }
}