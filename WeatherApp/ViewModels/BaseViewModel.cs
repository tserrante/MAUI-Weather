using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<TValue>(ref TValue backingField, TValue value, [CallerMemberName] string propertyName = "")
        {
            if(Comparer<TValue>.Default.Compare(backingField, value) == 0)
            { 
                return false; 
            }

            backingField = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
