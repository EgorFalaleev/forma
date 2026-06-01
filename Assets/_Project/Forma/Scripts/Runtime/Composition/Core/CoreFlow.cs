using System;
using System.Collections.Generic;
using Forma.Runtime.Core.Enemy;
using Forma.Runtime.Core.Features.Camera;
using Forma.Runtime.Core.Features.HexGrid;
using Forma.Runtime.Core.Features.Turret;
using Forma.Runtime.Input;
using Forma.Runtime.Player;
using Forma.Runtime.Services.Input;
using UnityEngine;
using VContainer.Unity;

namespace Forma.Runtime.Composition.Core
{
    public class CoreFlow
        : IStartable,
          ITickable,
          IDisposable
    {
        readonly IEnumerable<BaseInputService> _inputServices;
        readonly EnemyFlow _enemyFlow;
        readonly HexGridFlow _hexGridFlow;
        readonly TurretFlow _turretFlow;
        readonly CameraController _cameraController;
        readonly PlayerFactory _playerFactory;
        readonly MoveInputHandler _moveInputHandler;

        public CoreFlow(IEnumerable<BaseInputService> inputServices, EnemyFlow enemyFlow,
            HexGridFlow hexGridFlow, TurretFlow turretFlow,
            CameraController cameraController, PlayerFactory playerFactory,
            MoveInputHandler moveInputHandler)
        {
            _inputServices = inputServices;
            _enemyFlow = enemyFlow;
            _hexGridFlow = hexGridFlow;
            _turretFlow = turretFlow;
            _cameraController = cameraController;
            _playerFactory = playerFactory;
            _moveInputHandler = moveInputHandler;
        }

        public void Start()
        {
            Debug.Log("CoreFlow.Start()");

            _moveInputHandler.Enable();
            
            _playerFactory.Create(new Vector3(0f, 1f, 0f));

            _enemyFlow.Initialize();
            _hexGridFlow.Initialize();
            _turretFlow.Initialize();
            _cameraController.Initialize();

            foreach (BaseInputService inputService in _inputServices)
                inputService.Enable();
        }

        public void Tick()
        {
            _enemyFlow.Tick();
            _hexGridFlow.Tick();
            _turretFlow.Tick();
        }

        public void Dispose()
        {
            _moveInputHandler.Disable();
            
            foreach (BaseInputService inputService in _inputServices)
                inputService.Disable();

            _enemyFlow.Dispose();
            _hexGridFlow.Dispose();
            _turretFlow.Dispose();
        }
    }
}
