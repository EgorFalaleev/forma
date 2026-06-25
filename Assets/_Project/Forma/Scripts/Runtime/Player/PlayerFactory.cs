using Forma.Runtime.Common;
using Forma.Runtime.Components.MoveInput;
using Forma.Runtime.Player.Configs;
using UnityEngine;

namespace Forma.Runtime.Player
{
    public class PlayerFactory
    {
        readonly IMoveInput _moveInput;
        readonly PlayerConfig _playerConfig;

        public PlayerFactory(IMoveInput moveInput, PlayerConfig playerConfig)
        {
            _moveInput = moveInput;
            _playerConfig = playerConfig;
        }

        public Player Create(Vector3 spawnPosition)
        {
            var resource = Resources.Load<Player>(Constants.Resources.Player);

            Player player = Object.Instantiate(
                resource,
                spawnPosition,
                Quaternion.identity
            );

            player.Construct(_moveInput, _playerConfig);

            return player;
        }
    }
}
