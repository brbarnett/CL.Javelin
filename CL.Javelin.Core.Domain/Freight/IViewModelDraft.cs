using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CL.Javelin.Core.Domain.Freight
{
    public interface IViewModelDraft : INotifyPropertyChanged
    {
        bool IsDraft { get; set; }
    }
}
