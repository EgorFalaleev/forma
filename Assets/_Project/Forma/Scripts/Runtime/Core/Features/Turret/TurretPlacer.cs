using System;
using Cysharp.Threading.Tasks;
using Forma.Runtime.Core.Features.HexGrid.Tile.Abstract;
using Forma.Runtime.Core.Features.Turret.Abstract;
using Forma.Runtime.Core.Features.Turret.Configs;
using Forma.Runtime.Core.Features.Turret.Views;
using Forma.Runtime.HexGrid.Data;
using UnityEngine;

namespace Forma.Runtime.Core.Features.Turret
{
    public class TurretPlacer : IDisposable
    {
        public event Action<Turret> OnTurretPlaced;

        readonly ITurretInput _turretInput;
        readonly TurretFactory _turretFactory;
        readonly TurretViewFactory _turretViewFactory;
        readonly TurretViewAnimator _turretViewAnimator;
        readonly TurretConfig _turretConfig;
        readonly IHexTileController _hexTileController;

        bool _isPlacing;

        public TurretPlacer(ITurretInput turretInput, TurretFactory turretFactory,
            TurretViewFactory turretViewFactory,
            TurretViewAnimator turretViewAnimator, TurretConfig turretConfig,
            IHexTileController hexTileController)
        {
            _turretInput = turretInput;
            _turretFactory = turretFactory;
            _turretViewFactory = turretViewFactory;
            _turretViewAnimator = turretViewAnimator;
            _turretConfig = turretConfig;
            _hexTileController = hexTileController;
        }

        public void Initialize()
        {
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

            // if (!_hexTileSelectionProvider.SelectedTile.HasValue)
                // return;

            _isPlacing = true;

            // Vector3 selectedHexPosition =
                // _hexTileSelectionProvider.SelectedTile.Value.Position;

            // HexCubeCoordinates selectedHexCoordinates =
                // _hexTileSelectionProvider.SelectedTile.Value.Coordinates;

            // await _hexTileController.OccupyTile(selectedHexCoordinates);

            // TurretView turretView = _turretViewFactory.Create(
                // selectedHexPosition + _turretConfig.SpawnOffset
            // );

            // await _turretViewAnimator.PlaySpawnAnimation(turretView, selectedHexPosition);

            // Turret turret = _turretFactory.Create(turretView, selectedHexPosition);

            // OnTurretPlaced?.Invoke(turret);

            _isPlacing = false;
        }
    }
}
