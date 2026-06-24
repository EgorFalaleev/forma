using Forma.Runtime.HexGrid.Data;
using Forma.Runtime.Player;
using UnityEngine;

namespace Forma.Runtime.Turret
{
    public class TurretController
    {
        readonly TurretFactory _turretFactory;
        readonly PlayerRepository _playerRepository;
        readonly TurretRepository _turretRepository;
        readonly Transform _turretsParent;

        public TurretController(TurretFactory turretFactory,
            PlayerRepository playerRepository, TurretRepository turretRepository)
        {
            _turretFactory = turretFactory;
            _playerRepository = playerRepository;
            _turretRepository = turretRepository;
            _turretsParent = new GameObject("Turrets").transform;
        }

        public void PlaceTurret(Vector2 positionXZ, HexCubeCoordinates coordinates)
        {
            var spawnPosition = new Vector3(
                positionXZ.x,
                _playerRepository.Transform.position.y,
                positionXZ.y
            );

            Turret turret = _turretFactory.Create(spawnPosition, _turretsParent);

            _turretRepository.Register(turret, coordinates);

            turret.gameObject.SetActive(false);

            turret.PlaySpawnAnimation();
        }
    }
}
