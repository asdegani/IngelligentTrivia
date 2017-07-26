using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TriviaGame.Generators;

namespace TriviaGame.Components
{
    public class GameManager : IGameManager
    {

        private List<IQuestionGenerator> generators;  

        public void init()
        {
            generators = new List<IQuestionGenerator>();
            generators.Add(new SampleGenerator());
        }

        public string getNextAction(string userResponse)
        {
            return "";
        }
        
    }
}