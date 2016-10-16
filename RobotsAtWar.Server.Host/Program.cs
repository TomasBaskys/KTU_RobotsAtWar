using System;
using System.IO;
using log4net.Config;
using Topshelf;

namespace RobotsAtWar.Server.Host
{
    class Program
    {
        static void Main()
        {
            XmlConfigurator.Configure(new FileInfo("..\\..\\App.config"));
            
            HostFactory.Run(x =>
            {
                x.Service<BattleEngine>(s =>
                {
                    s.ConstructUsing(name => new BattleEngine(new Uri("http://localhost:1235/")));
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();
                x.StartManually();
                x.SetDescription("RobotsAtWar");
                x.SetDisplayName("RobotsAtWar");
                x.SetServiceName("RobotsAtWar");
            });


        }
    }
}
