using System;
using UnityEngine;

namespace Forma.Runtime.Services.Input
{
    public interface IHexClickInput
    {
        event Action<Vector2> OnClicked;
    }
}
