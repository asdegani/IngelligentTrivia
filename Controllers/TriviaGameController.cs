using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TriviaGame.Models;

namespace TriviaGame.Controllers
{
    [RoutePrefix("api/TriviaGame")]
    public class TriviaGameController : ApiController
    {
        [Route("UserResponse")]
        [System.Web.Http.HttpPost]
        public string UserResponse(UserResponseModel response)
        {
            //TODO
            return "TODO";
        }
    }
}