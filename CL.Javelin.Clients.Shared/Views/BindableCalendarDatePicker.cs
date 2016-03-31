using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CL.Javelin.Clients.Shared.Views
{
    public class BindableCalendarDatePicker : CalendarDatePicker, INotifyPropertyChanged, IDisposable
    {
        public static readonly DependencyProperty SelectedValueProperty = 
                            DependencyProperty.Register("SelectedValue",
                                                        typeof(DateTimeOffset),
                                                        typeof(BindableCalendarDatePicker),
                                                        new PropertyMetadata(DateTimeOffset.MinValue, OnSelectedValuePropertyChanged)
                                                       );

        private static void OnSelectedValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BindableCalendarDatePicker control = (BindableCalendarDatePicker) d;
            control.OnSelectedValueChanged(e);
        }

        public DateTimeOffset SelectedValue
        {
            get { return (DateTimeOffset) this.GetValue(SelectedValueProperty); }
            set { this.SetValue(SelectedValueProperty, value); }
        }

        private void OnSelectedValueChanged(DependencyPropertyChangedEventArgs e)
        {
            this.Date = (DateTimeOffset)e.NewValue;
        }

        public BindableCalendarDatePicker()
        {
            this.Closed += OnCalendarPickerClosed;
        }

        private void OnCalendarPickerClosed(object sender, object e)
        {
            // ReSharper disable once ExplicitCallerInfoArgument
            this.SelectedValue =
                (DateTimeOffset)
                    (this.Date.HasValue ? this.Date.Value : SelectedValueProperty.GetMetadata(GetType()).DefaultValue);
        }


        public void Dispose()
        {
            this.Closed -= OnCalendarPickerClosed;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
