using System;

namespace Forma.Runtime.Core.Features.Turret.Abstract
{
    public interface ITurretInput
    {
        event Action OnPlaceTurretClicked;
    }
}
