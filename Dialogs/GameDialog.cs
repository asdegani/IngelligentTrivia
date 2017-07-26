using Microsoft.Bot.Builder.Dialogs;
using System.Text.RegularExpressions;
using TriviaGame.Components;

namespace Dialogs
{
    public class GameDialog
    {
        private static GameManager gameManager;

        public static readonly IDialog<string> Dialog = Chain

           .PostToChain()
           .Select(m => m.Text)
           .Switch
           (
               Chain.Case
               (
                   new Regex("^start"),
                   (context, text) =>

               Chain.Return("Your first question is...")
               .PostToUser()
               .ContinueWith<string, string>(async (ctx, res) =>
               {
                   gameManager = new GameManager();
                   gameManager.Init();

                   var response = await res;

                   var question = gameManager.GetNextQuestion();

                   return Chain.Return(question);

               })

               ),
               Chain.Default<string, IDialog<string>>(

                   (context, text) =>

                       Chain.Return(gameManager.GetNextAction(text))
               )
           )
           .Unwrap().PostToUser();
    }
}