using System;
using UnityEngine;

namespace Forma.Runtime.Player
{
    public class PlayerRepository
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
