using System;
using System.Collections.Generic;
using Microsoft.SharePoint.Client;
using NDesk.Options;

namespace ReleaseCandidateManager
{
    public class ListCandidatesStateAction : IAction
    {
        private string site;
        private string listName;

        public int Perform(OptionSet optionSet)
        {
            using (var clientContext = new ClientContext(site))
            {
                var list = clientContext.Web.Lists.GetByTitle(listName);

                var camlQuery = new CamlQuery();
                var items = list.GetItems(camlQuery);
                clientContext.Load(items);
                clientContext.ExecuteQuery();

                foreach (ListItem item in items)
                {
                    Console.WriteLine("{0} {1}", item["PackageVersion"], item["PackageState"]);
                }
            }
            return 0;
        }

        public bool VerifyParamaters()
        {
            return !string.IsNullOrEmpty(site)
                  && !string.IsNullOrEmpty(listName);
        }

        public IEnumerable<CommandLineParameterBinding> BindCommadLineParameters()
        {
            yield return new CommandLineParameterBinding("s|site=", "Site", x => site = x);
            yield return new CommandLineParameterBinding("l|list=", "List", x => listName = x);
        }
    }
}