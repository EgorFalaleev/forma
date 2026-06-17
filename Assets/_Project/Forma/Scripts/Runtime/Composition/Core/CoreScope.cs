using System;
using Forma.Runtime.Camera;
using Forma.Runtime.Enemies;
using Forma.Runtime.HexGrid;
using Forma.Runtime.HexGrid.Configs;
using Forma.Runtime.Input;
using Forma.Runtime.Player;
using Forma.Runtime.Turret;
using Forma.Runtime.Turret.Configs;
using Forma.Runtime.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using EnemyFactory = Forma.Runtime.Enemies.EnemyFactory;

namespace Forma.Runtime.Composition.Core
{
    public class CoreScope : LifetimeScope
    {
        [SerializeField] CameraController _cameraController;

        [SerializeField] HexGridConfig _hexGridConfig;
        [SerializeField] TurretConfig _turretConfig;
        [SerializeField] WavesConfig _wavesConfig;
        [SerializeField] EnemyConfig _enemyConfig;
        [SerializeField] PlayerConfig _playerConfig;

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

            builder.RegisterInstance(_hexGridConfig);
            builder.RegisterInstance(_turretConfig);
            builder.RegisterInstance(_wavesConfig);
            builder.RegisterInstance(_enemyConfig);
            builder.RegisterInstance(_playerConfig);

            RegisterServices(builder);

            RegisterHexGrid(builder);
            RegisterPlayer(builder);
            RegisterCamera(builder);
            RegisterEnemy(builder);
            RegisterTurret(builder);
        }

        void RegisterCamera(IContainerBuilder builder)
        {
            builder.RegisterComponent(_cameraController);
        }

        void RegisterTurret(IContainerBuilder builder)
        {
            builder
               .Register<TurretRepository>(Lifetime.Singleton)
               .AsSelf();

            builder
               .Register<TurretFactory>(Lifetime.Singleton)
               .AsSelf();

            builder
               .Register<TurretController>(Lifetime.Singleton)
               .AsSelf();
        }

        void RegisterEnemy(IContainerBuilder builder)
        {
            builder
               .Register<EnemyFactory>(Lifetime.Singleton)
               .AsSelf();

            builder
               .Register<EnemyRepository>(Lifetime.Singleton)
               .AsSelf();

            builder
               .Register<EnemySpawner>(Lifetime.Singleton)
               .AsSelf();

            builder
               .Register<EnemyController>(Lifetime.Singleton)
               .AsSelf();
        }

        void RegisterPlayer(IContainerBuilder builder)
        {
            builder
               .Register<PlayerFactory>(Lifetime.Singleton)
               .AsSelf();

            builder
               .Register<PlayerRepository>(Lifetime.Singleton)
               .As<IPlayerProvider>()
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
               .Register<PlaceTurretInputHandler>(Lifetime.Singleton)
               .AsImplementedInterfaces()
               .AsSelf();
        }
    }
}
