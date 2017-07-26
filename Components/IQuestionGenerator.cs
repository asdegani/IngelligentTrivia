using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaGame.Models;

namespace TriviaGame.Components
{
    public interface IQuestionGenerator
    {
        List<QA> GenerateQuestions(IList<KeyValuePair<string, string>> typeAndQueryPairs); 
    }
}
