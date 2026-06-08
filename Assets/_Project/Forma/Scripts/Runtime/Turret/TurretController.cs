using Forma.Runtime.Player;
using UnityEngine;

namespace Forma.Runtime.Turret
{
    public class TurretController
    {
        readonly TurretFactory _turretFactory;
        readonly PlayerRepository _playerRepository;
        readonly Transform _turretsParent;

        public TurretController(TurretFactory turretFactory,
            PlayerRepository playerRepository)
        {
            _turretFactory = turretFactory;
            _playerRepository = playerRepository;
            _turretsParent = new GameObject("Turrets").transform;
        }

        public void PlaceTurret(Vector2 positionXZ)
        {
            var spawnPosition = new Vector3(
                positionXZ.x,
                _playerRepository.Transform.position.y,
                positionXZ.y
            );
            
            Turret turret = _turretFactory.Create(spawnPosition, _turretsParent);

            turret.gameObject.SetActive(false);
            
            turret.PlaySpawnAnimation();
        }
    }
}
