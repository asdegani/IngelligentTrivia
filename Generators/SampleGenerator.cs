using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using Google.Apis.Kgsearch.v1;
using Google.Apis.Kgsearch.v1.Data;
using Google.Apis.Services;
using TriviaGame.Components;
using TriviaGame.Models;

namespace TriviaGame.Generators
{
    public class SampleGenerator : IQuestionGenerator
    {
        public List<QA> GenerateQuestions()
        {
            throw new NotImplementedException();
        }

        public string Test(string query)
        {
            KgsearchService service =
                new Google.Apis.Kgsearch.v1.KgsearchService(new BaseClientService.Initializer
                {
                    ApplicationName = "trivia-hackathon-2017", // "Intelligent Trivia Game",
                    ApiKey = "AIzaSyArbQptaVvPHUQv-8VuA85WjEJMkfOZgjw"
                });
            EntitiesResource.SearchRequest search = service.Entities.Search();
            search.Indent = true;
            search.Languages = "en";
            search.Limit = 10;
            search.Types = "Person";
            search.Query = "London";
            SearchResponse result = search.Execute();

            return result.ItemListElement.Aggregate("", (current, item) => current + item.ToString());
        }

        public string Test2()
        {
            var client = new HttpClient();

            var response = client.SendAsync(new HttpRequestMessage(HttpMethod.Get,
                new Uri(
                    "https://en.wikipedia.org/w/api.php?action=query&titles=Israel&prop=revisions&rvprop=content&format=json"))).Result;

            return response.Content.ReadAsStringAsync().Result;
        }
    }
}