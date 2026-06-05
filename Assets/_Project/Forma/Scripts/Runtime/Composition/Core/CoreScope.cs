using System;
using Forma.Runtime.Core.Common;
using Forma.Runtime.Core.Enemy;
using Forma.Runtime.Core.Enemy.Abstract;
using Forma.Runtime.Core.Enemy.Configs;
using Forma.Runtime.Core.Enemy.Views;
using Forma.Runtime.Core.Features.Camera;
using Forma.Runtime.Core.Features.Movement;
using Forma.Runtime.Core.Features.Turret;
using Forma.Runtime.Core.Features.Turret.Abstract;
using Forma.Runtime.Core.Features.Turret.Configs;
using Forma.Runtime.Core.Features.Turret.Views;
using Forma.Runtime.HexGrid;
using Forma.Runtime.HexGrid.Configs;
using Forma.Runtime.Input;
using Forma.Runtime.Player;
using Forma.Runtime.Services.CameraProvider;
using Forma.Runtime.Services.Input;
using Forma.Runtime.Services.Time;
using Forma.Runtime.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Forma.Runtime.Composition.Core
{
    public class CoreScope : LifetimeScope
    {
        [SerializeField] EnemyView _enemyViewPrefab;
        [SerializeField] TurretView _turretViewPrefab;
        [SerializeField] CameraView _cameraView;

        [SerializeField] HexGridConfig _hexGridConfig;
        [SerializeField] EnemyConfig _enemyConfig;
        [SerializeField] TurretConfig _turretConfig;

        [SerializeField] GameStatePanel _gameStatePanel;

        protected override void Configure(IContainerBuilder builder)
        {
            Register(builder);

            builder.RegisterEntryPoint<CoreFlow>();
        }

        void Register(IContainerBuilder builder)
        {
            builder
               .RegisterInstance(_gameStatePanel)
               .As<GameStatePanel>();

            builder
               .RegisterInstance(_enemyViewPrefab)
               .As<EnemyView>();

            builder
               .RegisterInstance(_turretViewPrefab)
               .As<TurretView>();

            builder
               .RegisterInstance(_cameraView)
               .As<CameraView>();

            builder.RegisterInstance(_hexGridConfig);
            builder.RegisterInstance(_enemyConfig);
            builder.RegisterInstance(_turretConfig);

            RegisterServices(builder);

            RegisterHexGrid(builder);
            RegisterPlayer(builder);
            RegisterCamera(builder);
            RegisterEnemy(builder);
            RegisterTurret(builder);
        }

        void RegisterCamera(IContainerBuilder builder)
        {
            builder
               .Register<CameraController>(Lifetime.Singleton)
               .As<IDisposable>()
               .AsSelf();
        }

        void RegisterTurret(IContainerBuilder builder)
        {
            builder
               .Register<TurretInputService>(Lifetime.Singleton)
               .As<BaseInputService>()
               .As<ITurretInput>();

            builder
               .Register<TurretViewAnimator>(Lifetime.Singleton)
               .AsSelf();

            builder
               .Register<TurretPlacer>(Lifetime.Singleton)
               .AsSelf();

            builder
               .Register<TurretViewFactory>(Lifetime.Singleton)
               .AsSelf();

            builder
               .Register<TurretFactory>(Lifetime.Singleton)
               .AsSelf();

            builder
               .Register<TurretFlow>(Lifetime.Singleton)
               .AsSelf();
        }

        void RegisterEnemy(IContainerBuilder builder)
        {
            builder
               .Register<EnemyViewFactory>(Lifetime.Singleton)
               .AsSelf();

            builder
               .Register<EnemyFactory>(Lifetime.Singleton)
               .AsSelf();

            builder
               .Register<EnemyFlow>(Lifetime.Singleton)
               .As<IEnemyRegistry>()
               .AsSelf();
        }

        void RegisterPlayer(IContainerBuilder builder)
        {
            builder
               .Register<PlayerFactory>(Lifetime.Singleton)
               .AsSelf();

            builder
               .Register<PlayerRepository>(Lifetime.Singleton)
               .AsSelf();
        }

        void RegisterHexGrid(IContainerBuilder builder)
        {
            builder
               .Register<TileFactory>(Lifetime.Singleton)
               .AsSelf();

            builder
               .Register<GridRepository>(Lifetime.Singleton)
               .AsSelf();

            builder
               .Register<GridBuilder>(Lifetime.Singleton)
               .AsSelf();

            builder
               .Register<GridAnimator>(Lifetime.Singleton)
               .AsSelf();

            builder
               .Register<GridController>(Lifetime.Singleton)
               .AsSelf();

            builder
               .Register<TileSelector>(Lifetime.Singleton)
               .AsSelf();

            builder
               .Register<TileController>(Lifetime.Singleton)
               .AsSelf();
        }

        void RegisterServices(IContainerBuilder builder)
        {
            builder
               .Register<CameraProvider>(Lifetime.Singleton)
               .As<ICameraProvider>();

            builder.Register<InputActions>(Lifetime.Singleton);

            builder
               .Register<MoveInputHandler>(Lifetime.Singleton)
               .AsImplementedInterfaces()
               .AsSelf();

            builder
               .Register<ToggleGridInputHandler>(Lifetime.Singleton)
               .AsImplementedInterfaces()
               .AsSelf();

            builder
               .Register<ClickGridTileInputHandler>(Lifetime.Singleton)
               .AsImplementedInterfaces()
               .AsSelf();

            builder
               .Register<UnityTimeService>(Lifetime.Singleton)
               .As<ITimeService>();

            builder
               .Register<PlayerTargetProvider>(Lifetime.Singleton)
               .As<ITargetProvider>()
               .As<ITargetSetter>();
        }
    }
}
