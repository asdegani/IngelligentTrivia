using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Helpers;
using Google.Apis.Kgsearch.v1;
using Google.Apis.Kgsearch.v1.Data;
using Google.Apis.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TriviaGame.Components;
using TriviaGame.Models;

namespace TriviaGame.Generators
{
    using System.IO;
    using System.Xml;

    public class SampleGenerator : IQuestionGenerator
    {
        private readonly string GoogleKnowledgeApiKey; 
        public SampleGenerator()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"AppSettings.config");
            Hashtable hashtable = GetSettings(path);
            GoogleKnowledgeApiKey = (string) hashtable["googleKnowledgeApiKey"]; 
        }

        public List<QA> GenerateQuestions(IList<KeyValuePair<string, string>> typeAndQueryPairs)
        {
            IEnumerator<KeyValuePair<string, string>> enumerator = typeAndQueryPairs.GetEnumerator();

            List <QA> qaList = new List<QA>();
            while (enumerator.MoveNext())
            {
                KeyValuePair<string, string> typeAndQueryPair = enumerator.Current;
                SummarySearchResult summarySearchResult = findSummarySearchResult(typeAndQueryPair);
                qaList.Add(generateQuestionFromSummaryResults(summarySearchResult));
            }
            
            return qaList; 
        }

        private QA generateQuestionFromSummaryResults(SummarySearchResult summary)
        {
            using (var httpClient = new HttpClient())
            {
                var url = "http://10.164.85.76:8000/questiongenerator/api/generateQuestionAnswerPairFromText";
                var data = new { text = summary.summary };
                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8,
                    "application/json");
                HttpResponseMessage response = httpClient.PostAsync(url, stringContent).Result;

                if (response != null && response.IsSuccessStatusCode)
                {
                    object content = JsonConvert.DeserializeObject(
                        response.Content.ReadAsStringAsync()
                        .Result);
                    JObject jObject = JObject.Parse(JsonConvert.SerializeObject(content));

                    string question = (string)jObject["question"];
                    string answer = (string)jObject["answer"];
                    return new QA(question, answer, new List<string>());
                }
            }
            return null;
        } 

        private SummarySearchResult findSummarySearchResult(KeyValuePair<string, string> typeAndQueryPair)
        {
            KgsearchService service =
                new Google.Apis.Kgsearch.v1.KgsearchService(new BaseClientService.Initializer
                {
                    ApplicationName = "trivia-hackathon-2017", // "Intelligent Trivia Game",
                    ApiKey = GoogleKnowledgeApiKey//
                });
            EntitiesResource.SearchRequest search = service.Entities.Search();
            search.Indent = true;
            search.Languages = "en";
            search.Limit = 10;
            search.Types = typeAndQueryPair.Key;
            search.Query = typeAndQueryPair.Value;
            SearchResponse response = search.Execute();
            IEnumerator<object> enumerator = (IEnumerator<object>)response.ItemListElement.GetEnumerator();
            List<SummarySearchResult> summaries = new List<SummarySearchResult>();
            while (enumerator.MoveNext())
            {
                object value = enumerator.Current;

                JObject jObject = JObject.Parse(JsonConvert.SerializeObject(value));
                JToken result = jObject["result"];
                string name = (string)result["name"];
                double resultScore = (double)jObject["resultScore"];
                string summary;
                if (result["detailedDescription"] != null)
                {
                    summary = (string) result["detailedDescription"]["articleBody"];
                    summaries.Add(new SummarySearchResult(name, summary, resultScore));
                }  
            }

            return filterBestSummaryResult(summaries);
        }

        private Hashtable GetSettings(string path)
        {
            Hashtable hashtable = new Hashtable();
            if (File.Exists(path))
            {
                StreamReader reader = new StreamReader
                (
                    new FileStream(
                        path,
                        FileMode.Open,
                        FileAccess.Read,
                        FileShare.Read)
                );
                XmlDocument doc = new XmlDocument();
                string xmlIn = reader.ReadToEnd();
                reader.Close();
                doc.LoadXml(xmlIn);
                foreach (XmlNode child in doc.ChildNodes)
                    if (child.Name.Equals("Settings"))
                        foreach (XmlNode node in child.ChildNodes)
                            if (node.Name.Equals("add"))
                                hashtable.Add
                                (
                                    node.Attributes["key"].Value,
                                    node.Attributes["value"].Value
                                );
            }
            return (hashtable);
        }

        private SummarySearchResult filterBestSummaryResult(List<SummarySearchResult> summaries)
        {
            return summaries[0]; //TODO
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