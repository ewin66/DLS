
namespace ServerProgram
{
    using System;
    using System.Collections.Generic;


    public class ExecutedEventArgs : EventArgs
    {

        public StaticCommand Command { get; private set; }

        public object Parameter { get; private set; }

        internal ExecutedEventArgs(StaticCommand command, object parameter)
        {
            Command = command;
            Parameter = parameter;
        }
    }
}
