using Forma.Runtime.Movement;
using Forma.Runtime.Projectiles.Configs;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Movement _movement;
    [SerializeField] Attack _attack;

    IMoveInput _moveInput;

    public void Construct(ProjectileConfig projectileConfig, IMoveInput moveInput)
    {
        _moveInput = moveInput;

        _movement.Construct(projectileConfig.Movement);
        _attack.Construct(projectileConfig.Attack);
    }

    void Update()
    {
        _movement.Move(_moveInput.MoveDirection);
    }
}