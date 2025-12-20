using Forma.Runtime.Core.Features.Movement;
using Forma.Runtime.Core.Player;
using Forma.Runtime.Services.Input;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Forma.Runtime.Core
{
    public class CoreScope : LifetimeScope
    {
        [SerializeField] PlayerView _playerView;

        protected override void Configure(IContainerBuilder builder)
        {
            Register(builder);

            builder.RegisterEntryPoint<CoreFlow>();
        }

        void Register(IContainerBuilder builder)
        {
            builder.Register<InputActions>(Lifetime.Singleton);

            builder.Register<MoveInputService>(Lifetime.Singleton)
                .As<BaseInputService>()
                .As<IMoveInput>();
            
            builder.Register<MovementController>(Lifetime.Singleton).As<ITickable>();
            builder.RegisterInstance(_playerView).As<IMovableView>();
        }
    }
}