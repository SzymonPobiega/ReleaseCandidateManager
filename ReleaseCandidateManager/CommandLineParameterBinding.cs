using System;

namespace ReleaseCandidateManager
{
    public class CommandLineParameterBinding
    {
        private readonly string prototype;
        private readonly string description;
        private readonly Action<string> bindAction;

        public CommandLineParameterBinding(string prototype, string description, Action<string> bindAction)
        {
            this.prototype = prototype;
            this.bindAction = bindAction;
            this.description = description;
        }

        public Action<string> BindAction
        {
            get { return bindAction; }
        }

        public string Description
        {
            get { return description; }
        }

        public string Prototype
        {
            get { return prototype; }
        }
    }
}