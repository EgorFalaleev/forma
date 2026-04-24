using System;
using UnityEngine;

namespace Forma.Runtime.Core.Features.HexGrid
{
    public interface IHexClickInput
    {
        event Action<Vector2> OnClicked;
    }
}
