using Forma.Runtime.Services.Input;
using VContainer;
using VContainer.Unity;

namespace Forma.Runtime.Core
{
    public class CoreScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<InputActions>(Lifetime.Singleton);

            builder.Register<MoveInputService>(Lifetime.Singleton)
                .As<BaseInputService>()
                .As<IMoveInput>();

            builder.RegisterEntryPoint<CoreFlow>();
        }
    }
}