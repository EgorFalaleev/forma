using Forma.Runtime.Core.Player.Views;
using UnityEngine;

namespace Forma.Runtime.Core.Player
{
    public class PlayerViewFactory
    {
        readonly PlayerView _playerViewPrefab;

        public PlayerViewFactory(PlayerView playerViewPrefab)
        {
            _playerViewPrefab = playerViewPrefab;
        }

        public PlayerView Create()
        {
            PlayerView playerView = Object.Instantiate(_playerViewPrefab);

            return playerView;
        }
    }
}
