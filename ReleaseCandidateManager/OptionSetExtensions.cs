using System;
using System.Linq;
using NDesk.Options;

namespace ReleaseCandidateManager
{
    public static class OptionSetExtensions
    {
        public static OptionSet AddFromActions(this OptionSet optionSet, params IAction[] actions)
        {
            var bindings = actions.SelectMany(x => x.BindCommadLineParameters());
            var groupedByProto = bindings.GroupBy(x => new { x.Prototype, x.Description});
            foreach (var group in groupedByProto)
            {
                var localGroup = group;
                Action<string> combinedAction = x =>
                                                    {
                                                        var individualActions = localGroup.ToList();
                                                        foreach (var binding in individualActions)
                                                        {
                                                            binding.BindAction(x);
                                                        }
                                                    };
                optionSet.Add(localGroup.Key.Prototype, localGroup.Key.Description, combinedAction);
            }
            return optionSet;
        }
    }
}