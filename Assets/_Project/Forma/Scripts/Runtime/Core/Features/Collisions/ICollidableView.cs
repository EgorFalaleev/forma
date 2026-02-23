using System;
using UnityEngine;

namespace Forma.Runtime.Core.Features.Collisions
{
    public interface ICollidableView
    {
        public event Action<Collider> OnCollided;
    }
}