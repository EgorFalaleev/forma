using System;
using Forma.Runtime.Core.Common;
using Forma.Runtime.Core.Enemy;
using Forma.Runtime.Core.Enemy.Abstract;
using Forma.Runtime.Core.Enemy.Configs;
using Forma.Runtime.Core.Enemy.Views;
using Forma.Runtime.Core.Features.Camera;
using Forma.Runtime.Core.Features.HexGrid;
using Forma.Runtime.Core.Features.HexGrid.Configs;
using Forma.Runtime.Core.Features.HexGrid.Grid;
using Forma.Runtime.Core.Features.HexGrid.Grid.Abstract;
using Forma.Runtime.Core.Features.HexGrid.Tile;
using Forma.Runtime.Core.Features.HexGrid.Tile.Abstract;
using Forma.Runtime.Core.Features.HexGrid.Views;
using Forma.Runtime.Core.Features.Movement;
using Forma.Runtime.Core.Features.Turret;
using Forma.Runtime.Core.Features.Turret.Abstract;
using Forma.Runtime.Core.Features.Turret.Configs;
using Forma.Runtime.Core.Features.Turret.Views;
using Forma.Runtime.Core.Player;
using Forma.Runtime.Core.Player.Configs;
using Forma.Runtime.Core.Player.Views;
using Forma.Runtime.Services.CameraProvider;
using Forma.Runtime.Services.Input;
using Forma.Runtime.Services.Time;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Forma.Runtime.Composition.Core
{
    public class CoreScope : LifetimeScope
    {
        [SerializeField] PlayerView _playerViewPrefab;
        [SerializeField] EnemyView _enemyViewPrefab;
        [SerializeField] TurretView _turretViewPrefab;
        [SerializeField] CameraView _cameraView;

        [SerializeField] HexGridConfig _hexGridConfig;
        [SerializeField] PlayerConfig _playerConfig;
        [SerializeField] EnemyConfig _enemyConfig;
        [SerializeField] TurretConfig _turretConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            Register(builder);

            builder.RegisterEntryPoint<CoreFlow>();
        }

        void Register(IContainerBuilder builder)
        {
            builder
               .RegisterInstance(_playerViewPrefab)
               .As<PlayerView>();

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
            builder.RegisterInstance(_playerConfig);
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
               .Register<PlayerViewFactory>(Lifetime.Singleton)
               .AsSelf();

            builder
               .Register<PlayerFactory>(Lifetime.Singleton)
               .AsSelf();

            builder
               .Register<PlayerFlow>(Lifetime.Singleton)
               .AsSelf();
        }

        void RegisterHexGrid(IContainerBuilder builder)
        {
            builder
               .Register<ToggleGridInputService>(Lifetime.Singleton)
               .As<BaseInputService>()
               .As<IToggleGridInput>();

            builder
               .Register<HexTileSelectionProvider>(Lifetime.Singleton)
               .As<IHexTileSelectionProvider>()
               .As<IHexTileSelectionSetter>();

            builder
               .Register<HexTileClickInputService>(Lifetime.Singleton)
               .As<BaseInputService>()
               .As<IHexTileClickInput>();

            builder
               .Register<HexGridBuilder>(Lifetime.Singleton)
               .AsSelf();

            builder
               .Register<HexViewFactory>(Lifetime.Singleton)
               .AsSelf();

            builder
               .Register<HexGridRegistry>(Lifetime.Singleton)
               .As<IHexGridRegistry>()
               .AsSelf();

            builder
               .Register<HexGridAnimator>(Lifetime.Singleton)
               .As<IHexGridAnimator>()
               .AsSelf();

            builder
               .Register<HexTileAnimator>(Lifetime.Singleton)
               .AsSelf();

            builder
               .Register<HexTileSelector>(Lifetime.Singleton)
               .As<IHexTileDeselector>()
               .As<IHexTileSelectEvents>()
               .AsSelf();

            builder
               .Register<HexTileSelectionController>(Lifetime.Singleton)
               .AsSelf();

            builder
               .Register<HexTileOccupancyController>(Lifetime.Singleton)
               .AsSelf();

            builder
               .Register<HexTileController>(Lifetime.Singleton)
               .As<IHexTileController>()
               .AsSelf();

            builder
               .Register<StatesGraph>(Lifetime.Singleton)
               .AsSelf();

            builder
               .Register<HexGridFlow>(Lifetime.Singleton)
               .As<IHexGridEvents>()
               .AsSelf();
        }

        void RegisterServices(IContainerBuilder builder)
        {
            builder.Register<InputActions>(Lifetime.Singleton);

            builder
               .Register<MoveInputService>(Lifetime.Singleton)
               .As<BaseInputService>()
               .As<IMoveInput>();

            builder
               .Register<UnityTimeService>(Lifetime.Singleton)
               .As<ITimeService>();

            builder
               .Register<PlayerTargetProvider>(Lifetime.Singleton)
               .As<ITargetProvider>()
               .As<ITargetSetter>();

            builder
               .Register<CameraProvider>(Lifetime.Singleton)
               .As<ICameraProvider>();
        }
    }
}
