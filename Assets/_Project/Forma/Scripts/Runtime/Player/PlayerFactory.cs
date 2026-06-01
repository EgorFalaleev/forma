using Forma.Runtime.Common;
using Forma.Runtime.Core.Common;
using Forma.Runtime.Movement;
using UnityEngine;

namespace Forma.Runtime.Player
{
    public class PlayerFactory
    {
        readonly IMoveInput _moveInput;
        readonly ITargetSetter _targetSetter;

        public PlayerFactory(IMoveInput moveInput, ITargetSetter targetSetter)
        {
            _moveInput = moveInput;
            _targetSetter = targetSetter;
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

            _targetSetter.SetTarget(player.transform);

            return player;
        }
    }
}
