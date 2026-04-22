using System.Collections.Generic;
using Forma.Runtime.Core.Enemy;
using Forma.Runtime.Core.Features.HexGrid;
using Forma.Runtime.Core.Features.HexGrid.Configs;
using Forma.Runtime.Core.Player;
using Forma.Runtime.Services.CameraProvider;
using Forma.Runtime.Services.Input;
using Forma.Runtime.Services.TargetProvider;
using Forma.Runtime.Services.Time;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Forma.Runtime.Core
{
    public class CoreScope : LifetimeScope
    {
        [SerializeField] PlayerView _playerView;
        [SerializeField] EnemyView[] _enemyViews;
        [SerializeField] HexGridConfig _hexGridConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            Register(builder);

            builder.RegisterEntryPoint<CoreFlow>();
        }

        void Register(IContainerBuilder builder)
        {
            builder.RegisterInstance(_playerView);
            builder.RegisterInstance(_hexGridConfig);

            builder
               .RegisterInstance(_enemyViews)
               .As<IEnumerable<EnemyView>>();

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
               .WithParameter(_playerView.transform);

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
