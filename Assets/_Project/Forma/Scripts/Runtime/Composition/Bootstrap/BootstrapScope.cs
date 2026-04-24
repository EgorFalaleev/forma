using VContainer;
using VContainer.Unity;

namespace Forma.Runtime.Composition.Bootstrap
{
    public class BootstrapScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<BootstrapFlow>();
        }
    }
}