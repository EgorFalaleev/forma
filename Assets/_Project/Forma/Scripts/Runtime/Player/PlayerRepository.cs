using System;
using Forma.Runtime.Core.Common;
using UnityEngine;

namespace Forma.Runtime.Player
{
    public class PlayerRepository : ITargetProvider
    {
        public Transform Transform => _player.transform;
        
        Player _player;

        public void Register(Player player)
        {
            if (_player != null)
            {
                throw new Exception("Player is already registered.");
            }

            _player = player;
        }

        public void Unregister()
        {
            _player = null;
        }
    }
}
