namespace Forma.Runtime.Services.Collisions.Types
{
    public readonly struct DamageCollision : ICollision
    {
        public int Damage { get; }

        public DamageCollision(int damage)
        {
            Damage = damage;
        }
    }
}