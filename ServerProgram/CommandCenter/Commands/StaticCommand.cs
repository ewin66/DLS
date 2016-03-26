using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServerProgram
{
    public class StaticCommand
    {

        static StaticCommand()
        {
            CommandCache = new Dictionary<string, StaticCommand>();
        }

        public static Dictionary<string, StaticCommand> CommandCache { get; private set; }

        public static StaticCommand FindCommand(string commandName)
        {
            if (string.IsNullOrEmpty(commandName))
                return null;

            StaticCommand cmd = null;

            if (CommandCache.TryGetValue(commandName, out cmd))
                return cmd;

            return null;
        }


        public string CommandName { get; private set; }


        public event EventHandler<ExecutedEventArgs> Executed;

        public event EventHandler<CanExecuteEventArgs> CanExecuteChanged;


        public StaticCommand(string commandName)
        {

            CommandName = commandName;
            CommandCache.Add(commandName, this);
        }


        public void Execute(object parameter)
        {
            if (this.CanExecute(parameter))
            {
                if (this.Executed != null)
                {
                    ExecutedEventArgs e = new ExecutedEventArgs(this, parameter);
                    this.Executed(this, e);
                }
            }
        }

        public bool CanExecute(object parameter)
        {
            if (this.CanExecuteChanged != null)
            {
                CanExecuteEventArgs e = new CanExecuteEventArgs(this, parameter);
                this.CanExecuteChanged(this, e);
                return e.CanExecute;
            }

            return true;
        }


    }
}
