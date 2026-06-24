using Forma.Runtime.Movement;
using Forma.Runtime.Projectiles.Configs;
using R3;
using UnityEngine;

namespace Forma.Runtime.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] Movement.Movement _movement;
        [SerializeField] Attack.Attack _attack;

        IMoveInput _moveInput;
        readonly CompositeDisposable _disposables = new();

        public void Construct(ProjectileConfig projectileConfig, IMoveInput moveInput)
        {
            _moveInput = moveInput;

            _movement.Construct(projectileConfig.Movement);
            _attack.Construct(projectileConfig.Attack);

            _attack
               .OnHit
               .Subscribe(OnHit)
               .AddTo(_disposables);
        }

        void Update()
            => _movement.Move(_moveInput.MoveDirection);

        void OnDestroy()
            => _disposables.Dispose();

        void OnHit(Unit _)
            => Destroy(gameObject);
    }
}
