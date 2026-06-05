using System;
using Forma.Runtime.Core.Enemy;
using Forma.Runtime.Core.Features.Camera;
using Forma.Runtime.Core.Features.Turret;
using Forma.Runtime.Core.StateMachine;
using Forma.Runtime.GameStates;
using Forma.Runtime.HexGrid;
using Forma.Runtime.Input;
using Forma.Runtime.Player;
using Forma.Runtime.UI;
using UnityEngine;
using VContainer.Unity;

namespace Forma.Runtime.Composition.Core
{
    public class CoreFlow
        : IStartable,
          ITickable,
          IDisposable
    {
        readonly GameStatePanel _gameStatePanel;
        readonly EnemyFlow _enemyFlow;
        readonly TurretFlow _turretFlow;
        readonly CameraController _cameraController;
        readonly StateMachine _gameStateMachine;
        readonly GameStatesGraph _gameStatesGraph;

        public CoreFlow(PlayerFactory playerFactory, PlayerRepository playerRepository,
            MoveInputHandler moveInputHandler,
            ToggleGridInputHandler toggleGridInputHandler, GridController gridController,
            GameStatePanel gameStatePanel,
            ClickGridTileInputHandler clickGridTileInputHandler,
            TileController tileController)
        {
            _gameStatePanel = gameStatePanel;

            _gameStatesGraph = new GameStatesGraph(
                playerFactory,
                playerRepository,
                moveInputHandler,
                toggleGridInputHandler,
                gridController,
                clickGridTileInputHandler,
                tileController
            );

            _gameStateMachine = _gameStatesGraph.StateMachine;
        }

        public void Start()
        {
            Debug.Log("CoreFlow.Start()");

            // _enemyFlow.Initialize();
            // _turretFlow.Initialize();
            // _cameraController.Initialize();

            _gameStatePanel.Construct(_gameStateMachine);

            _gameStatesGraph.EnterInitialState();
        }

        public void Tick()
        {
            // _enemyFlow.Tick();
            // _turretFlow.Tick();

            _gameStateMachine.Tick();
        }

        public void Dispose()
        {
            // _enemyFlow.Dispose();
            // _turretFlow.Dispose();
        }
    }
}
