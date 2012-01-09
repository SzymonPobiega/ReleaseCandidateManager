using System;
using System.Collections.Generic;
using NDesk.Options;

namespace ReleaseCandidateManager
{
    public class PrintHelpAction : IAction
    {
        public int Perform(OptionSet optionSet)
        {
            optionSet.WriteOptionDescriptions(Console.Out);
            return 0;
        }

        public bool VerifyParamaters()
        {
            return true;
        }

        public IEnumerable<CommandLineParameterBinding> BindCommadLineParameters()
        {
            yield break;
        }
    }
}