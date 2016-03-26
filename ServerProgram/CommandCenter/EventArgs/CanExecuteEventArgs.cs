
namespace ServerProgram
{
    using System;
    using System.Collections.Generic;

    public class CanExecuteEventArgs : EventArgs
    {

        public StaticCommand Command { get; private set; }

        public object Parameter { get; private set; }

        public bool CanExecute { get; set; }

        internal CanExecuteEventArgs(StaticCommand command, object parameter)
        {
            Command = command;
            Parameter = parameter;
        }

    }
}
