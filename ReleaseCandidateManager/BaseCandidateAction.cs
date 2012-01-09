using System.Collections.Generic;
using NDesk.Options;

namespace ReleaseCandidateManager
{
    public abstract class BaseCandidateAction : IAction
    {
        protected string site;
        protected string version;
        protected string listName;

        public abstract int Perform(OptionSet optionSet);

        public virtual bool VerifyParamaters()
        {
            return !string.IsNullOrEmpty(site)
                   && !string.IsNullOrEmpty(version)
                   && !string.IsNullOrEmpty(listName);
        }

        public virtual IEnumerable<CommandLineParameterBinding> BindCommadLineParameters()
        {
            yield return new CommandLineParameterBinding("s|site=", "Site", x => site = x);
            yield return new CommandLineParameterBinding("v|version=", "Version", x => version = x);
            yield return new CommandLineParameterBinding("l|list=", "List", x => listName = x);
        }
    }
}