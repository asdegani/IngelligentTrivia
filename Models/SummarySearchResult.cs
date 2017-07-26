using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TriviaGame.Models
{
    public class SummarySearchResult
    {
        public SummarySearchResult(string name, string summary, double resultScore)
        {
            this.name = name;
            this.summary = summary;
            this.resultScore = resultScore;
        }

        public string name { get; set; }
        //private IEnumerable<string> type { get; set; }
        private string summary { get; set; }
        private double resultScore { get; set; }

    }
}