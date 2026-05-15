using System;
using System.Collections.Generic;
using Forma.Runtime.Core.Enemy;
using Forma.Runtime.Core.Features.Camera;
using Forma.Runtime.Core.Features.HexGrid;
using Forma.Runtime.Core.Features.Turret;
using Forma.Runtime.Core.Player;
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
        readonly PlayerFlow _playerFlow;
        readonly EnemyFlow _enemyFlow;
        readonly HexGridFlow _hexGridFlow;
        readonly TurretFlow _turretFlow;
        readonly CameraController _cameraController;

        public CoreFlow(IEnumerable<BaseInputService> inputServices, EnemyFlow enemyFlow,
            PlayerFlow playerFlow, HexGridFlow hexGridFlow, TurretFlow turretFlow,
            CameraController cameraController)
        {
            _inputServices = inputServices;
            _enemyFlow = enemyFlow;
            _playerFlow = playerFlow;
            _hexGridFlow = hexGridFlow;
            _turretFlow = turretFlow;
            _cameraController = cameraController;
        }

        public void Start()
        {
            Debug.Log("CoreFlow.Start()");

            _playerFlow.Initialize();
            _enemyFlow.Initialize();
            _hexGridFlow.Initialize();
            _turretFlow.Initialize();
            _cameraController.Initialize();

            foreach (BaseInputService inputService in _inputServices)
                inputService.Enable();
        }

        public void Tick()
        {
            _playerFlow.Tick();
            _enemyFlow.Tick();
            _hexGridFlow.Tick();
            _turretFlow.Tick();
        }

        public void Dispose()
        {
            foreach (BaseInputService inputService in _inputServices)
                inputService.Disable();

            _playerFlow.Dispose();
            _enemyFlow.Dispose();
            _hexGridFlow.Dispose();
            _turretFlow.Dispose();
        }
    }
}
