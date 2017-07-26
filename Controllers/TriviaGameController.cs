using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TriviaGame.Components;
using TriviaGame.Generators;
using TriviaGame.Models;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using System.Threading.Tasks;
using System.Net;

namespace TriviaGame.Controllers
{
    [BotAuthentication]
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
        public async Task<HttpResponseMessage> UserResponse([FromBody]Activity activity)
        {
            var connector = new ConnectorClient(new Uri(activity.ServiceUrl));

            if (activity.Type == ActivityTypes.Message)
            {
                await Conversation.SendAsync(activity, () => Dialogs.GameDialog.Dialog);
            }
            else
            {
                //HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }
    }
}