using System;
using Cysharp.Threading.Tasks;
using Forma.Runtime.Core.Features.HexGrid;
using Forma.Runtime.Core.Features.HexGrid.Data;
using Forma.Runtime.Core.Features.Turret.Configs;
using Forma.Runtime.Core.Features.Turret.Views;
using UnityEngine;

namespace Forma.Runtime.Core.Features.Turret
{
    public class TurretPlacer : IDisposable
    {
        public event Action<Turret> OnTurretPlaced;

        readonly ITurretInput _turretInput;
        readonly IHexSelectionProvider _hexSelectionProvider;
        readonly IHexTileDeselector _hexTileDeselector;
        readonly TurretFactory _turretFactory;
        readonly TurretViewFactory _turretViewFactory;
        readonly TurretViewAnimator _turretViewAnimator;
        readonly TurretConfig _turretConfig;
        readonly HexTileController _hexTileController;

        bool _isPlacing;

        public TurretPlacer(ITurretInput turretInput, TurretFactory turretFactory,
            TurretViewFactory turretViewFactory,
            IHexSelectionProvider hexSelectionProvider,
            IHexTileDeselector hexTileDeselector, TurretViewAnimator turretViewAnimator,
            TurretConfig turretConfig, HexTileController hexTileController)
        {
            _turretInput = turretInput;
            _turretFactory = turretFactory;
            _turretViewFactory = turretViewFactory;
            _hexSelectionProvider = hexSelectionProvider;
            _hexTileDeselector = hexTileDeselector;
            _turretViewAnimator = turretViewAnimator;
            _turretConfig = turretConfig;
            _hexTileController = hexTileController;

            _turretInput.OnPlaceTurretClicked += OnPlaceTurretClicked;
        }

        public void Dispose()
        {
            _turretInput.OnPlaceTurretClicked -= OnPlaceTurretClicked;
        }

        void OnPlaceTurretClicked()
        {
            TryPlaceTurret()
               .Forget();
        }

        async UniTask TryPlaceTurret()
        {
            if (_isPlacing)
                return;

            if (!_hexSelectionProvider.SelectedHexPosition.HasValue
             || !_hexSelectionProvider.SelectedHexCoordinates.HasValue)
                return;

            _isPlacing = true;

            Vector3 selectedHexPosition = _hexSelectionProvider.SelectedHexPosition.Value;

            HexCubeCoordinates selectedHexCoordinates =
                _hexSelectionProvider.SelectedHexCoordinates.Value;

            await _hexTileDeselector.DeselectTile();

            _hexTileController.OccupyTile(selectedHexCoordinates);

            TurretView turretView = _turretViewFactory.Create(
                selectedHexPosition + _turretConfig.SpawnOffset
            );

            await _turretViewAnimator.PlaySpawnAnimation(turretView, selectedHexPosition);

            Turret turret = _turretFactory.Create(turretView, selectedHexPosition);

            _turretViewAnimator.PlayInfiniteRotation(turretView);

            OnTurretPlaced?.Invoke(turret);

            _isPlacing = false;
        }
    }
}
