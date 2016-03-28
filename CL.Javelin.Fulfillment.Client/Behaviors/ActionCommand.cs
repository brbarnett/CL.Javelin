using System;
using System.Windows.Input;

namespace CL.Javelin.Fulfillment.Client.Behaviors
{
    public class ActionCommand<TParam> : ICommand
    {
        private readonly Action<TParam> _action;

        public ActionCommand(Action<TParam> action)
        {
            this._action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if(parameter.GetType() != typeof(TParam)) return;

            this._action((TParam)parameter);
        }
    }
}
