using System;
using Forma.Runtime.Core.Enemy;
using Forma.Runtime.Core.Features.Camera;
using Forma.Runtime.Core.Features.HexGrid;
using Forma.Runtime.Core.Features.Turret;
using Forma.Runtime.Core.StateMachine;
using Forma.Runtime.GameStates;
using Forma.Runtime.Input;
using Forma.Runtime.Player;
using UnityEngine;
using VContainer.Unity;

namespace Forma.Runtime.Composition.Core
{
    public class CoreFlow
        : IStartable,
          ITickable,
          IDisposable
    {
        readonly EnemyFlow _enemyFlow;
        readonly HexGridFlow _hexGridFlow;
        readonly TurretFlow _turretFlow;
        readonly CameraController _cameraController;
        readonly StateMachine _gameStateMachine;
        readonly GameStatesGraph _gameStatesGraph;

        public CoreFlow(EnemyFlow enemyFlow, HexGridFlow hexGridFlow,
            TurretFlow turretFlow, CameraController cameraController,
            PlayerFactory playerFactory, PlayerRepository playerRepository,
            MoveInputHandler moveInputHandler)
        {
            _enemyFlow = enemyFlow;
            _hexGridFlow = hexGridFlow;
            _turretFlow = turretFlow;
            _cameraController = cameraController;

            _gameStatesGraph = new GameStatesGraph(
                playerFactory,
                playerRepository,
                moveInputHandler
            );

            _gameStateMachine = _gameStatesGraph.StateMachine;
        }

        public void Start()
        {
            Debug.Log("CoreFlow.Start()");

            // _enemyFlow.Initialize();
            // _hexGridFlow.Initialize();
            // _turretFlow.Initialize();
            // _cameraController.Initialize();

            _gameStatesGraph.EnterInitialState();
        }

        public void Tick()
        {
            // _enemyFlow.Tick();
            // _hexGridFlow.Tick();
            // _turretFlow.Tick();

            _gameStateMachine.Tick();
        }

        public void Dispose()
        {
            // _enemyFlow.Dispose();
            // _hexGridFlow.Dispose();
            // _turretFlow.Dispose();
        }
    }
}
