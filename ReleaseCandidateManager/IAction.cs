using System.Collections.Generic;
using NDesk.Options;

namespace ReleaseCandidateManager
{
    public interface IAction
    {
        int Perform(OptionSet optionSet);
        bool VerifyParamaters();
        IEnumerable<CommandLineParameterBinding> BindCommadLineParameters();
    }
}