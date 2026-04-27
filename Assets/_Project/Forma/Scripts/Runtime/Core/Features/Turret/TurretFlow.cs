using System;
using System.Collections.Generic;

namespace Forma.Runtime.Core.Features.Turret
{
    public class TurretFlow : IDisposable
    {
        readonly TurretPlacer _turretPlacer;
        readonly List<Turret> _turrets;

        public TurretFlow(TurretPlacer turretPlacer)
        {
            _turretPlacer = turretPlacer;

            _turrets = new List<Turret>();
        }

        public void Initialize()
        {
            _turretPlacer.OnTurretPlaced += OnTurretPlaced;
        }

        public void Tick()
        {
            foreach (Turret turret in _turrets)
            {
                turret.Tick();
            }
        }

        public void Dispose()
        {
            _turretPlacer.OnTurretPlaced -= OnTurretPlaced;

            _turretPlacer.Dispose();
        }

        void OnTurretPlaced(Turret turret)
        {
            _turrets.Add(turret);
        }
    }
}
