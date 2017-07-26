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

        private string question;
        private List<String> wrongAnswers;
        private string correctAnswer; 
    }
}