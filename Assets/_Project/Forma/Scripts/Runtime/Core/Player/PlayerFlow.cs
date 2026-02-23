using System;

namespace Forma.Runtime.Core.Player
{
    public class PlayerFlow : IDisposable
    {
        readonly PlayerFactory _playerFactory;

        Player _player;
        
        public PlayerFlow(PlayerFactory playerFactory)
        {
            _playerFactory = playerFactory;
        }

        public void Initialize()
        {
            _player = _playerFactory.Create();
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