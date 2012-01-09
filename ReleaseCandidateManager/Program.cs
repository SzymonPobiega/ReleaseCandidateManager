using System;
using System.Linq;
using System.Text;
using NDesk.Options;

namespace ReleaseCandidateManager
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var createCandidateAction = new CreateCandidateAction();
                var updateCandidateStateAction = new UpdateCandidateStateAction();
                var listCandidatesAction = new ListCandidatesStateAction();
                var printHelpAction = new PrintHelpAction();

                IAction selectedAction = printHelpAction;

                var optionSet = new OptionSet()
                    .Add("h|?|help", "Print this help information", x => selectedAction = printHelpAction)
                    .Add("a|action=", "Action to be performed (create/update)",
                         delegate(string x)
                             {
                                 switch (x.ToLowerInvariant())
                                 {
                                     case "create":
                                         selectedAction = createCandidateAction;
                                         break;
                                     case "update":
                                         selectedAction = updateCandidateStateAction;
                                         break;
                                     case "list":
                                         selectedAction = listCandidatesAction;
                                         break;
                                     default:
                                         selectedAction = printHelpAction;
                                         break;
                                 }
                             })
                    .AddFromActions(printHelpAction, createCandidateAction, updateCandidateStateAction, listCandidatesAction);

                optionSet.Parse(args);
                var result = selectedAction.Perform(optionSet);
                Environment.Exit(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
