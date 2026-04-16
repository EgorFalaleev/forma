using UnityEngine;

namespace Forma.Runtime.Services.TargetProvider
{
    public class PlayerTargetProvider : ITargetProvider
    {
        public Transform Target => _player;

        Transform _player;

        public PlayerTargetProvider(Transform player)
        {
            _player = player;
        }
    }
}
