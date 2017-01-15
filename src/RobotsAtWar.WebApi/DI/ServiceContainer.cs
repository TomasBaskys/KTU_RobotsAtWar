using Autofac;

namespace RobotsAtWar.WebApi.DI
{
    public static class ServicesContainer
    {
        public static IContainer Container;

        static ServicesContainer()
        {
            CreateContainer();
        }

        private static void CreateContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<RobotReaderModule>();

            Container = builder.Build();
        }
    }
}