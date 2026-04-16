using System.Collections.Generic;
using Forma.Runtime.Core.Enemy;
using Forma.Runtime.Core.Features.HexGrid;
using Forma.Runtime.Core.Features.HexGrid.Data;
using Forma.Runtime.Core.Player;
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
        [SerializeField] HexGridData _hexGridData;

        protected override void Configure(IContainerBuilder builder)
        {
            Register(builder);

            builder.RegisterEntryPoint<CoreFlow>();
        }

        void Register(IContainerBuilder builder)
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
               .Register<UnityTimeService>(Lifetime.Singleton)
               .As<ITimeService>();

            builder.RegisterInstance(_playerView);
            builder.RegisterInstance(_hexGridData);

            builder
               .Register<PlayerFactory>(Lifetime.Singleton)
               .AsSelf();

            builder
               .RegisterInstance(_enemyViews)
               .As<IEnumerable<EnemyView>>();

            builder
               .Register<EnemyFactory>(Lifetime.Singleton)
               .AsSelf();

            builder
              .Register<HexGridFactory>(Lifetime.Singleton)
              .AsSelf();

            builder
               .Register<PlayerTargetProvider>(Lifetime.Singleton)
               .As<ITargetProvider>()
               .WithParameter(_playerView.transform);

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
