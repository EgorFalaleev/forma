using Forma.Runtime.Components;
using Forma.Runtime.Components.MoveInput;
using Forma.Runtime.Enemies.Configs;
using R3;
using UnityEngine;

namespace Forma.Runtime.Enemies
{
    public class Enemy : MonoBehaviour
    {
        public Health Health => _health;
        
        [SerializeField] RigidbodyMovement _movement;
        [SerializeField] Attack _attack;
        [SerializeField] Health _health;

        readonly CompositeDisposable _disposables = new();
        IMoveInput _moveInput;

        public void Construct(IMoveInput moveInput, EnemyConfig enemyConfig)
        {
            _moveInput = moveInput;

            _movement.Construct(enemyConfig.Movement);
            _attack.Construct(enemyConfig.Attack);
            _health.Construct(enemyConfig.Health);

            _health
               .OnDied
               .Subscribe(Die)
               .AddTo(_disposables);
        }

        void FixedUpdate()
            => _movement.Move(_moveInput.MoveDirection);

        void Die(Unit _)
            => Destroy(gameObject);
    }
}
