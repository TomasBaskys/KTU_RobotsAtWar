﻿using System;
using System.Web.Http;
using System.Web.Http.SelfHost;
using RobotsAtWar.Server;

namespace RobotsAtWar.WebApi
{
    public class Startup
    {
        private readonly HttpSelfHostServer _server;

        public Startup(Uri address)
        {
            var configuration = new HttpSelfHostConfiguration(address);
            configuration.MessageHandlers.Add(new CustomHeaderHandler());

            RegisterRoutes(configuration);

            _server = new HttpSelfHostServer(configuration);
        }

        private static void RegisterRoutes(HttpSelfHostConfiguration configuration)
        {
            configuration.Routes.MapHttpRoute(
                name: "Default",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { controller = RouteParameter.Optional },
                constraints: null
                );
        }

        public void Start()
        {
            BattleFields.LoadBattleField();
            _server.OpenAsync();
        }

        public void Stop()
        {
            _server.CloseAsync().Wait();
            _server.Dispose();
        }
    }
}
