using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using TriviaGame.Components;
using TriviaGame.Controllers;
using Unity.WebApi;

namespace TriviaGame.App_Start
{
    public static class UnityConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();

            // Your mappings here
            container.RegisterType<IGameManager, GameManager>();
            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}