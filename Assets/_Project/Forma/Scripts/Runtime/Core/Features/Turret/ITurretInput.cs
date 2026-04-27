using System;

namespace Forma.Runtime.Core.Features.Turret
{
    public interface ITurretInput
    {
        event Action OnPlaceTurretClicked;
    }
}
