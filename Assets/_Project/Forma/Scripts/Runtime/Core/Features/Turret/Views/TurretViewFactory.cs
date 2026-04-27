using UnityEngine;

namespace Forma.Runtime.Core.Features.Turret.Views
{
    public class TurretViewFactory
    {
        readonly TurretView _turretViewPrefab;

        public TurretViewFactory(TurretView turretViewPrefab)
        {
            _turretViewPrefab = turretViewPrefab;
        }

        public TurretView Create(Vector3 position)
        {
            TurretView turretView = Object.Instantiate(
                _turretViewPrefab,
                position,
                Quaternion.identity
            );

            return turretView;
        }
    }
}
