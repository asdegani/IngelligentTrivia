using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TriviaGame.Models
{
    public class QA
    {
        public QA(string question, string correctAnswer, List<string> wrongAnswers)
        {
            this.question = question;
            this.wrongAnswers = wrongAnswers;
            this.correctAnswer = correctAnswer;
        }

        public string question;
        public List<String> wrongAnswers;
        public string correctAnswer; 
    }
}