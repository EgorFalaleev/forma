using System.Collections.Generic;
using Forma.Runtime.HexGrid.Data;
using R3;

namespace Forma.Runtime.Turret
{
    public class TurretRepository
    {
        public Observable<Turret> OnTurretDestroyed => _onTurretDestroyed;

        readonly Dictionary<Turret, HexCubeCoordinates> _turrets = new();
        readonly Subject<Turret> _onTurretDestroyed = new();

        public void Register(Turret turret, HexCubeCoordinates coordinates)
        {
            _turrets.Add(turret, coordinates);
            turret.OnDied.Subscribe(FireTurretDestroyed);
        }

        public void Unregister(Turret turret)
            => _turrets.Remove(turret);

        public HexCubeCoordinates GetCoordinates(Turret turret)
            => _turrets[turret];

        void FireTurretDestroyed(Turret turret)
            => _onTurretDestroyed.OnNext(turret);
    }
}
