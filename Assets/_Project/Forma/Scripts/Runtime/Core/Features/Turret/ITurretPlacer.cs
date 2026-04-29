using System;
using Forma.Runtime.Core.Features.HexGrid.Data;

namespace Forma.Runtime.Core.Features.Turret
{
    public interface ITurretPlacer
    {
        event Action<HexCubeCoordinates> OnTurretReservedTile;
    }
}
