using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ICompAccounting.WpBank.ModelView
{
    public class DayOperationsMV: INotifyPropertyChanged
    {
        public DayOperationsMV()
        { 
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


        string _Title = "Банківські операції за день";
        public string Title 
        { 
            set
            {
                _Title = value;
                OnPropertyChanged("Title");
            }
            get
            {
                return _Title;
            }
        } 

    }
}
