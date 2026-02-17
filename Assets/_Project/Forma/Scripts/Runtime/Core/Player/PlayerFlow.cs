using Forma.Runtime.Core.Features.Movement;

namespace Forma.Runtime.Core.Player
{
    public class PlayerFlow
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
    }
}