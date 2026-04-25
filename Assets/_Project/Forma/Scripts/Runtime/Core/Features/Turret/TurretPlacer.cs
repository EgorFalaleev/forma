using System;
using Forma.Runtime.Core.Features.HexGrid;
using UnityEngine;

namespace Forma.Runtime.Core.Features.Turret
{
    public class TurretPlacer : IDisposable
    {
        public event Action<Turret> OnTurretPlaced;

        readonly ITurretInput _turretInput;
        readonly IHexSelectionProvider _hexSelectionProvider;
        readonly TurretFactory _turretFactory;
        readonly TurretViewFactory _turretViewFactory;

        public TurretPlacer(ITurretInput turretInput, TurretFactory turretFactory,
            TurretViewFactory turretViewFactory,
            IHexSelectionProvider hexSelectionProvider)
        {
            _turretInput = turretInput;
            _turretFactory = turretFactory;
            _turretViewFactory = turretViewFactory;
            _hexSelectionProvider = hexSelectionProvider;

            _turretInput.OnPlaceTurretClicked += TryPlaceTurret;
        }

        public void Dispose()
        {
            _turretInput.OnPlaceTurretClicked -= TryPlaceTurret;
        }

        void TryPlaceTurret()
        {
            if (_hexSelectionProvider.SelectedHexPosition.HasValue)
            {
                Vector3 selectedHexPosition =
                    _hexSelectionProvider.SelectedHexPosition.Value;

                TurretView turretView = _turretViewFactory.Create(selectedHexPosition);

                Turret turret = _turretFactory.Create(turretView, selectedHexPosition);

                OnTurretPlaced?.Invoke(turret);
            }
        }
    }
}
