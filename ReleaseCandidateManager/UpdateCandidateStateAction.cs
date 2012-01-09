using System;
using System.Collections.Generic;
using Microsoft.SharePoint.Client;
using NDesk.Options;

namespace ReleaseCandidateManager
{
    public class UpdateCandidateStateAction : BaseCandidateAction
    {
        private string state;

        public override int Perform(OptionSet optionSet)
        {
            using (var clientContext = new ClientContext(site))
            {
                var list = clientContext.Web.Lists.GetByTitle(listName);

                var camlQuery = new CamlQuery
                                    {
                                        ViewXml =
                                            string.Format("<View><Query><Where><Eq><FieldRef Name='PackageVersion'/>" +
                                                          "<Value Type='Text'>{0}</Value></Eq></Where></Query><RowLimit>10</RowLimit></View>",
                                                          version)
                                    };
                var items = list.GetItems(camlQuery);
                clientContext.Load(items);
                clientContext.ExecuteQuery();

                if (items.Count == 0)
                {
                    throw new InvalidOperationException("No such version: "+version);
                }
                if (items.Count > 1)
                {
                    throw new InvalidOperationException("Ambiguous version: " + version);
                }
                var item = items[0];
                item["PackageState"] = state;
                item.Update();
                clientContext.ExecuteQuery(); 
            }
            return 0;
        }

        public override bool VerifyParamaters()
        {
            return base.VerifyParamaters()
                   && !string.IsNullOrEmpty(state);
        }

        public override IEnumerable<CommandLineParameterBinding> BindCommadLineParameters()
        {
            foreach (var binding in base.BindCommadLineParameters())
            {
                yield return binding;
            }
            yield return new CommandLineParameterBinding("u|state=", "State", x => state = x);
        }
    }
}