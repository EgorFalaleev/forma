using Forma.Runtime.Core.Common;
using Forma.Runtime.Core.Enemy;
using Forma.Runtime.Core.Enemy.Configs;
using Forma.Runtime.Core.Enemy.Views;
using Forma.Runtime.Core.Features.HexGrid;
using Forma.Runtime.Core.Features.HexGrid.Configs;
using Forma.Runtime.Core.Features.Movement;
using Forma.Runtime.Core.Player;
using Forma.Runtime.Core.Player.Configs;
using Forma.Runtime.Core.Player.Views;
using Forma.Runtime.Services.CameraProvider;
using Forma.Runtime.Services.Input;
using Forma.Runtime.Services.TargetProvider;
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
        [SerializeField] HexGridConfig _hexGridConfig;
        [SerializeField] PlayerConfig _playerConfig;
        [SerializeField] EnemyConfig _enemyConfig;

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

            builder.RegisterInstance(_hexGridConfig);
            builder.RegisterInstance(_playerConfig);
            builder.RegisterInstance(_enemyConfig);

            RegisterInput(builder);
            RegisterServices(builder);
            RegisterFactories(builder);
            RegisterFlows(builder);
        }

        void RegisterServices(IContainerBuilder builder)
        {
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

        void RegisterInput(IContainerBuilder builder)
        {
            builder.Register<InputActions>(Lifetime.Singleton);

            builder
               .Register<MoveInputService>(Lifetime.Singleton)
               .As<BaseInputService>()
               .As<IMoveInput>();

            builder
               .Register<ToggleGridInputService>(Lifetime.Singleton)
               .As<BaseInputService>()
               .As<IToggleGridInput>();

            builder
               .Register<HexClickInputService>(Lifetime.Singleton)
               .As<BaseInputService>()
               .As<IHexClickInput>();
        }

        void RegisterFactories(IContainerBuilder builder)
        {
            builder
               .Register<PlayerViewFactory>(Lifetime.Singleton)
               .AsSelf();

            builder
               .Register<PlayerFactory>(Lifetime.Singleton)
               .AsSelf();

            builder
               .Register<EnemyFactory>(Lifetime.Singleton)
               .AsSelf();

            builder
               .Register<HexGridFactory>(Lifetime.Singleton)
               .AsSelf();
        }

        void RegisterFlows(IContainerBuilder builder)
        {
            builder
               .Register<PlayerFlow>(Lifetime.Singleton)
               .AsSelf();

            builder
               .Register<EnemyFlow>(Lifetime.Singleton)
               .AsSelf();

            builder
               .Register<HexGridFlow>(Lifetime.Singleton)
               .AsSelf();
        }
    }
}
