using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CL.Javelin.Core.Domain.Freight;
using CL.Javelin.Core.Utilities;
using Prism.Commands;
using Prism.Windows.Mvvm;

namespace CL.Javelin.Clients.Shared.ViewModels
{
    public class RequestViewModel : ViewModelBase, IRequest, IPropertyChangeTransaction
    {
        private readonly List<DelegateCommand> _commandsWhenIsValidChanges;

        public RequestViewModel(IRequest request, string serviceUri)
        {
            this.MinDate = DateTimeOffset.Now;
            this.MaxDate = DateTimeOffset.MaxValue;

            this._commandsWhenIsValidChanges = new List<DelegateCommand>();

            //order matters here
            Func<bool> isValid = () => this.IsValid;

            if (!string.IsNullOrEmpty(serviceUri))
            {
                this.DeleteCommand = new DelegateCommand(() => this.Delete(serviceUri), isValid);
                this._commandsWhenIsValidChanges.Add(this._commandDelete);

                this.UpdateCommand = new DelegateCommand(() => this.Update(serviceUri), isValid);
                this._commandsWhenIsValidChanges.Add(this._commandUpdate);

                this.AddCommand = new DelegateCommand(() => this.Add(serviceUri), isValid);
                this._commandsWhenIsValidChanges.Add(this._commandAdd);
            }
            
            new AbstractRequestCopier().Copy(request, this);
            //\
        }

        private Guid _id;

        public Guid Id
        {
            get { return this._id; }
            set { this.SetProperty(ref _id, value); }
        }

        private string _customer;
        public string Customer
        {
            get { return this._customer; }
            set
            {
                if (this.SetProperty(ref this._customer, value))
                {
                    this.OnIsValidChanged();
                }
            }
        }

        private string _origin;
        public string Origin
        {
            get { return this._origin; }
            set
            {
                if (this.SetProperty(ref this._origin, value))
                {
                    this.OnIsValidChanged();
                }
            }
        }

        private string _destination;
        public string Destination
        {
            get { return this._destination; }
            set
            {
                if(this.SetProperty(ref this._destination, value))
                {
                    this.OnIsValidChanged();
                }
            }
        }

        private static DateTimeOffset? _minDate;
        public DateTimeOffset? MinDate
        {
            get { return _minDate; }
            private set { this.SetProperty(ref _minDate, value); }
        }

        private static DateTimeOffset? _maxDate;
        public DateTimeOffset? MaxDate
        {
            get { return _maxDate; }
            private set { this.SetProperty(ref _maxDate, value); }
        }

        private DateTimeOffset? _deadLine;
        public DateTimeOffset? Deadline
        {
            get { return this._deadLine; }
            set { this.SetProperty(ref _deadLine, value); }
        }

        private bool _isOpen;
        public bool Open
        {
            get { return this._isOpen; }
            set { this.SetProperty(ref _isOpen, value); }
        }

        private bool IsValid
        {
            get
            {
                if (string.IsNullOrEmpty(this.Customer)) return false;
                if (string.IsNullOrEmpty(this.Origin)) return false;
                if (string.IsNullOrEmpty(this.Destination)) return false;

                return true;
            }
        }

        private int _weight;
        public int Weight
        {
            get { return this._weight; }
            set { this.SetProperty(ref _weight, value); }
        }
        private int _skids;
        public int Skids
        {
            get { return this._skids; }
            set { this.SetProperty(ref _skids, value); }
        }

        private int _pieces;
        public int Pieces
        {
            get { return this._pieces; }
            set { this.SetProperty(ref _pieces, value); }
        }

        private string _hazardClass;
        public string HazardClass
        {
            get { return this._hazardClass; }
            set { this.SetProperty(ref _hazardClass, value); }
        }
        
        public async void Delete(string serviceUri)
        {
            if (!this.IsValid)
            {
                return;
            }

            await Http.Delete(serviceUri + $"/{this.GetDomainRequest().Id}");
        }

        protected override bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            bool wasChanged = base.SetProperty(ref storage, value, propertyName);
            if (wasChanged)
            {
                if (!this._isInTransaction &&
                    this.Id != Guid.Empty &&
                    !ReferenceEquals(this.UpdateCommand, null) &&
                    this.UpdateCommand.CanExecute(this))
                {
                    this.UpdateCommand.Execute(this);
                }
            }
            return wasChanged;
        }

        public async void Update(string serviceUri)
        {
            if (this.IsValid)
            {
                Request request = this.GetDomainRequest();
                await Http.Post(serviceUri, request);
            }
        }

        public async void Add(string serviceUri)
        {
            if (this.IsValid)
            {
                this.Id = Guid.NewGuid();
                Request request = this.GetDomainRequest();
                await Http.Post(serviceUri, request);
            }
        }

        protected virtual void OnIsValidChanged()
        {
            // ReSharper disable once ExplicitCallerInfoArgument
            this.OnPropertyChanged("IsValid");

            if (!ReferenceEquals(this._commandsWhenIsValidChanges, null))
            {
                foreach (DelegateCommand command in this._commandsWhenIsValidChanges)
                {
                    command.RaiseCanExecuteChanged();
                }
            }
        }

        private DelegateCommand _commandDelete;
        public ICommand DeleteCommand
        {
            get { return this._commandDelete; }
            private set { this.SetProperty(ref this._commandDelete, (DelegateCommand) value); }
        }

        private DelegateCommand _commandUpdate;
        public ICommand UpdateCommand
        {
            get { return this._commandUpdate; }
            private set { this.SetProperty(ref this._commandUpdate, (DelegateCommand)value); }
        }

        private DelegateCommand _commandAdd;
        public ICommand AddCommand
        {
            get { return this._commandAdd; }
            private set { this.SetProperty(ref this._commandAdd, (DelegateCommand)value); }
        }

        public Request GetDomainRequest()
        {
            return new Request(this);
        }
        
        private bool _isInTransaction;

        void IPropertyChangeTransaction.BeginTransaction()
        {
            this._isInTransaction = true;
        }

        void IPropertyChangeTransaction.Commit()
        {
            this._isInTransaction = false;
        }
    }
}
