using System;
using Forma.Runtime.Core.Common;
using Forma.Runtime.Core.Player.Views;

namespace Forma.Runtime.Core.Player
{
    public class PlayerFlow : IDisposable
    {
        readonly PlayerFactory _playerFactory;
        readonly PlayerViewFactory _playerViewFactory;
        readonly ITargetSetter _targetSetter;

        Player _player;

        public PlayerFlow(PlayerFactory playerFactory,
            PlayerViewFactory playerViewFactory, ITargetSetter targetSetter)
        {
            _playerFactory = playerFactory;
            _playerViewFactory = playerViewFactory;
            _targetSetter = targetSetter;
        }

        public void Initialize()
        {
            PlayerView playerView = _playerViewFactory.Create();
            _targetSetter.SetTarget(playerView.transform);
            
            _player = _playerFactory.Create(playerView);
            
            _player.Initialize();
        }

        public void Tick()
        {
            _player.Tick();
        }

        public void Dispose()
        {
            _player?.Dispose();
        }
    }
}
