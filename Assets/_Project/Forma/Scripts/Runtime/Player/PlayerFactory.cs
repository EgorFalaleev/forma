using Forma.Runtime.Common;
using Forma.Runtime.Movement;
using UnityEngine;

namespace Forma.Runtime.Player
{
    public class PlayerFactory
    {
        readonly IMoveInput _moveInput;

        public PlayerFactory(IMoveInput moveInput)
        {
            _moveInput = moveInput;
        }

        public Player Create(Vector3 spawnPosition)
        {
            var resource = Resources.Load<Player>(Constants.Resources.Player);

            Player player = Object.Instantiate(
                resource,
                spawnPosition,
                Quaternion.identity
            );

            player.Construct(_moveInput);

            return player;
        }
    }
}
