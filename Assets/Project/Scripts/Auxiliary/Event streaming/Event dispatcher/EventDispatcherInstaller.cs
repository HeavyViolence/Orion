using Orion.Main.DI;

using VContainer;

namespace Orion.Auxiliary.EventStreaming
{
    public sealed class EventDispatcherInstaller : ServiceInstaller
    {
        public override void Install(IContainerBuilder builder)
        {
            builder.Register<EventDispatcher>(Lifetime.Singleton)
                   .AsImplementedInterfaces()
                   .AsSelf();
        }
    }
}