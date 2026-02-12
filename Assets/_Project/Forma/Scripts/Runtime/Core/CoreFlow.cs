using System;
using System.Collections.Generic;
using Forma.Runtime.Core.Enemy;
using Forma.Runtime.Core.Player;
using Forma.Runtime.Services.Input;
using UnityEngine;
using VContainer.Unity;

namespace Forma.Runtime.Core
{
    public class CoreFlow : IStartable, ITickable, IDisposable
    {
        readonly IEnumerable<BaseInputService> _inputServices;
        readonly IEnumerable<EnemyView> _enemyViews;
        readonly PlayerFlow _playerFlow;
        readonly EnemyFactory _enemyFactory;
        readonly EnemyFlow _enemyFlow;

        public CoreFlow(IEnumerable<BaseInputService> inputServices,
            IEnumerable<EnemyView> enemyViews, EnemyFactory enemyFactory,
            EnemyFlow enemyFlow, PlayerFlow playerFlow)
        {
            _inputServices = inputServices;
            _enemyViews = enemyViews;
            _enemyFactory = enemyFactory;
            _enemyFlow = enemyFlow;
            _playerFlow = playerFlow;
        }

        public void Start()
        {
            Debug.Log("CoreFlow.Start()");

            _playerFlow.Initialize();
            
            foreach (EnemyView enemyView in _enemyViews)
                _enemyFactory.Create(enemyView);

            foreach (BaseInputService inputService in _inputServices)
                inputService.Enable();
        }

        public void Tick()
        {
            _playerFlow.Tick();
            _enemyFlow.Tick();
        }

        public void Dispose()
        {
            foreach (BaseInputService inputService in _inputServices)
                inputService.Disable();
        }
    }
}