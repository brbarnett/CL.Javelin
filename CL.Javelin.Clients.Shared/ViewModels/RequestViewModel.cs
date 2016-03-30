using System;
using CL.Javelin.Core.Domain.Freight;
using Prism.Windows.Mvvm;

namespace CL.Javelin.Clients.Shared.ViewModels
{
    public class RequestViewModel : ViewModelBase, IRequest
    {
        private readonly Action _onPropertyChanged;

        public RequestViewModel(Action onPropertyChanged)
            : this(onPropertyChanged, null)
        {
            
        }

        public RequestViewModel(Action onPropertyChanged, IRequest request)
        {
            this._onPropertyChanged = onPropertyChanged;
            new AbstractRequestCopier().Copy(request, this);
        }

        protected override bool SetProperty<T>(ref T storage, T value, string propertyName = null)
        {
            bool wasChanged = base.SetProperty(ref storage, value, propertyName);
            if (wasChanged)
            {
                this._onPropertyChanged();
            }
            return wasChanged;
        }

        private Guid _id;

        public Guid Id
        {
            get { return this._id; }
            set { this.SetProperty(ref this._id, value); }
        }

        private string _customer;
        public string Customer
        {
            get { return this._customer; }
            set { this.SetProperty(ref this._customer, value); }
        }

        private string _origin;
        public string Origin
        {
            get { return this._origin; }
            set { this.SetProperty(ref this._origin, value); }
        }

        private string _destination;
        public string Destination
        {
            get { return this._destination; }
            set { this.SetProperty(ref this._destination, value); }
        }

        private DateTime _deadLine;
        public DateTime Deadline
        {
            get { return this._deadLine; }
            set { this.SetProperty(ref this._deadLine, value); }
        }

        private bool _isOpen;
        public bool Open
        {
            get { return this._isOpen; }
            set { this.SetProperty(ref this._isOpen, value); }
        }

        private int _weight;
        public int Weight
        {
            get { return this._weight; }
            set { this.SetProperty(ref this._weight, value); }
        }
        private int _skids;
        public int Skids
        {
            get { return this._skids; }
            set { this.SetProperty(ref this._skids, value); }
        }

        private int _pieces;
        public int Pieces
        {
            get { return this._pieces; }
            set { this.SetProperty(ref this._pieces, value); }
        }

        private string _hazardClass;
        public string HazardClass
        {
            get { return this._hazardClass; }
            set { this.SetProperty(ref this._hazardClass, value); }
        }
    }
}
