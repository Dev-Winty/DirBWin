using System;
using System.Windows.Input;

namespace DirbWin
{
    internal class Command : ICommand
    {
        private Action<object> executeMethod;
        private Func<object, bool> canExecuteMethod;

        public Command(Action<object> executeMethod, Func<object, bool> canExecuteMethod)
        {
            this.executeMethod = executeMethod;
            this.canExecuteMethod = canExecuteMethod;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object param)
        {
            return true;
        }

        public void Execute(object param)
        {
            executeMethod(param);
        }
    }
}