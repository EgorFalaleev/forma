using System;
using Forma.Runtime.Services.Collisions.Types;

namespace Forma.Runtime.Services.Collisions
{
    public interface ICollisionTriggers : IDisposable
    {
        event Action<ICollision> OnCollision;
    }
}