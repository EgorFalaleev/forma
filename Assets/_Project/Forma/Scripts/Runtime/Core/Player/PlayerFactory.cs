using Forma.Runtime.Core.Features.Movement;
using Forma.Runtime.Services.Input;
using Forma.Runtime.Services.Time;

namespace Forma.Runtime.Core.Player
{
    public class PlayerFactory
    {
        readonly IMoveInput _moveInput;
        readonly IMovableView _playerView;
        readonly ITimeService _timeService;

        public PlayerFactory(IMoveInput moveInput, IMovableView playerView,
            ITimeService timeService)
        {
            _moveInput = moveInput;
            _playerView = playerView;
            _timeService = timeService;
        }

        public MovementController Create()
        {
            return new MovementController(_moveInput, _playerView, _timeService);
        }
    }
}