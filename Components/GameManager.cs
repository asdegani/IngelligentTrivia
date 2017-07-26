using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TriviaGame.Generators;
using TriviaGame.Models;

namespace TriviaGame.Components
{
    public class GameManager : IGameManager
    {

        private List<IQuestionGenerator> generators;
        private List<KeyValuePair<string, string>> pairsList;
        private List<QA>.Enumerator qaEnumerator;


        public void init()
        {
            generators = new List<IQuestionGenerator>();
            generators.Add(new SampleGenerator());

            this.pairsList = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Person", "Obama"),
                new KeyValuePair<string, string>("Place", "New York"),
                new KeyValuePair<string, string>("SportsTeam", "Real Madrid")
            };
        }

        public string getNextAction(string userResponse)
        {
            switch (userResponse)
            {
                case "start":
                    this.qaEnumerator = this.generators.FirstOrDefault().GenerateQuestions(pairsList).GetEnumerator();
                    this.qaEnumerator.MoveNext();
                    return qaEnumerator.Current.question;

                default:
                    string answer = "";
                    if (TrueAnswer(userResponse))
                    {
                        answer += "CORRECT!\n";
                    }
                    else
                    {
                        answer += "The Correct Answer is:\n" + qaEnumerator.Current.correctAnswer + "\n";
                    }
                    if (qaEnumerator.MoveNext())
                    {
                        answer += qaEnumerator.Current.question;
                    }
                    else
                    {
                        answer += "GAME OVER!";
                    }
                    return answer;
            }
        }

        private bool TrueAnswer(string userResponse)
        {
            return qaEnumerator.Current.correctAnswer.Contains(userResponse);
        }
    }
}