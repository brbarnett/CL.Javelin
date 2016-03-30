using System;
using CL.Javelin.Core.Domain.Freight;
using Prism.Commands;
using Prism.Windows.Mvvm;

namespace CL.Javelin.Clients.Shared.ViewModels
{
    public class RequestFormViewModel : RequestViewModel
    {
        private readonly DelegateCommand[] _commandsToNotify;

        public DateTime Today { get; } = DateTime.Today.Date;

        public RequestFormViewModel(IRequest request, DelegateCommand[] commandsToNotify)
            : base(request)
        {
            this._commandsToNotify = commandsToNotify;
        }

        protected override bool SetProperty<T>(ref T storage, T value, string propertyName = null)
        {
            bool wasChanged = base.SetProperty(ref storage, value, propertyName);
            if (wasChanged)
            {
                this.NotifyCommands();
            }
            return wasChanged;
        }

        public bool IsValid()
        {
            if (String.IsNullOrEmpty(this.Customer)) return false;
            if (String.IsNullOrEmpty(this.Origin)) return false;
            if (String.IsNullOrEmpty(this.Destination)) return false;

            return true;
        }

        public bool IsDirty()
        {
            return false;
        }

        public void SetRequest(Request request)
        {
            new AbstractRequestCopier().Copy(request, this);
            this.NotifyCommands();
        }

        private void NotifyCommands()
        {
            if (!ReferenceEquals(this._commandsToNotify, null))
            {
                foreach (DelegateCommand command in this._commandsToNotify)
                {
                    command.RaiseCanExecuteChanged();
                }
            }
        }

        public Request GetDomainRequest()
        {
            return new Request(this);
        }
    }
}
