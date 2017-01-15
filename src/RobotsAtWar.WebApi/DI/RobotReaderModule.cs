using Autofac;
using RobotsAtWar.Server.Readers;

namespace RobotsAtWar.WebApi.DI
{
    public class RobotReaderModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new RobotReader())
                .AsSelf()
                .SingleInstance();
        }
    }
}