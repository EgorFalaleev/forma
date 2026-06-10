using Forma.Runtime.Core.Features.Camera;
using Forma.Runtime.Core.StateMachine.States;
using Forma.Runtime.Core.StateMachine.Triggers;
using Forma.Runtime.Player;
using UnityEngine;

namespace Forma.Runtime.GameStates
{
    public class StartBattleState : IState
    {
        public ITrigger OnBattleStarted => _onBattleStarted;
        
        readonly PlayerFactory _playerFactory;
        readonly PlayerRepository _playerRepository;
        readonly CameraController _cameraController;
        readonly Trigger _onBattleStarted;

        public StartBattleState(PlayerFactory playerFactory,
            PlayerRepository playerRepository, CameraController cameraController)
        {
            _playerFactory = playerFactory;
            _playerRepository = playerRepository;
            _cameraController = cameraController;

            _onBattleStarted = new Trigger();
        }

        public void OnEnter()
        {
            Player.Player player = _playerFactory.Create(new Vector3(10f, 1f, 0f));
            _playerRepository.Register(player);
            
            _cameraController.Initialize();
            
            _onBattleStarted.Fire();
        }

        public void OnExit() { }
    }
}
