using VContainer;
using VContainer.Unity;

namespace Forma.Runtime.Core
{
    public class CoreScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<CoreFlow>();
        }
    }
}