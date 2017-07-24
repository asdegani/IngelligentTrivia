using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaGame.Models;

namespace TriviaGame.Components
{
    interface IResponseValidator
    {
        double ValidateAnswer(QA qa, string answer); 
    }
}
