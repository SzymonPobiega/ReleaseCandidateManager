using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Principal;
using System.ServiceModel;
using Microsoft.SharePoint.Client;
using NDesk.Options;

namespace ReleaseCandidateManager
{
    public class CreateCandidateAction : BaseCandidateAction
    {
        private string title;
        private string triggerScript;

        public override int Perform(OptionSet optionSet)
        {
            using (var clientContext = new ClientContext(site))
            {
                var list = clientContext.Web.Lists.GetByTitle(listName);

                var itemCreateInfo = new ListItemCreationInformation();
                var item = list.AddItem(itemCreateInfo);
                item["Title"] = title;
                item["PackageVersion"] = version;

                item.Update();
                clientContext.ExecuteQuery();

                var fileName = Path.GetFileName(triggerScript);
                AddAttachment(item.Id.ToString(), fileName, System.IO.File.ReadAllBytes(triggerScript));
            }
            return 0;
        }

        private void AddAttachment(string itemId, string fileName, byte[] fileContent)
        {
            var binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Ntlm;
            var listsSvc = new ListServices.ListsSoapClient(binding, new EndpointAddress(site + "/_vti_bin/Lists.asmx"));
            listsSvc.ClientCredentials.Windows.AllowedImpersonationLevel = TokenImpersonationLevel.Impersonation;
            listsSvc.ClientCredentials.Windows.ClientCredential = CredentialCache.DefaultNetworkCredentials;
            listsSvc.AddAttachment(listName, itemId, fileName, fileContent);
        }

        public override bool VerifyParamaters()
        {
            return base.VerifyParamaters()
                   && !string.IsNullOrEmpty(title);
        }

        public override IEnumerable<CommandLineParameterBinding> BindCommadLineParameters()
        {
            foreach (var binding in base.BindCommadLineParameters())
            {
                yield return binding;
            }
            yield return new CommandLineParameterBinding("t|title=", "Title", x => title = x);
            yield return new CommandLineParameterBinding("r|trigger=", "Trigger script", x => triggerScript = x);
        }
    }
}