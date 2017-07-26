using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TriviaGame.Components;
using TriviaGame.Generators;
using TriviaGame.Models;

namespace TriviaGame.Controllers
{
    [RoutePrefix("api/TriviaGame")]
    public class TriviaGameController : ApiController
    {
        private readonly IGameManager GameManager;

        public TriviaGameController(IGameManager gameManager)
        {
            this.GameManager = gameManager;
        }

        [Route("UserResponse")]
        [System.Web.Http.HttpPost]
        public string UserResponse(UserResponseModel response)
        {
            //TODO
            return "TODO";
        }
    }
}