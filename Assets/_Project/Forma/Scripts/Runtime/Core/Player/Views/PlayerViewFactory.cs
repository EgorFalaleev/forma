using Forma.Runtime.Core.Player.Configs;
using UnityEngine;

namespace Forma.Runtime.Core.Player.Views
{
    public class PlayerViewFactory
    {
        readonly PlayerView _playerViewPrefab;
        readonly PlayerConfig _playerConfig;

        public PlayerViewFactory(PlayerView playerViewPrefab, PlayerConfig playerConfig)
        {
            _playerViewPrefab = playerViewPrefab;
            _playerConfig = playerConfig;
        }

        public PlayerView Create()
        {
            PlayerView playerView = Object.Instantiate(_playerViewPrefab);

            playerView.Initialize(_playerConfig.Damage.Amount);
            
            return playerView;
        }
    }
}
