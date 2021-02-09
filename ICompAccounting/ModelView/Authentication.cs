using ICompAccounting.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace ICompAccounting.ModelView
{
    public class AuthenticationModelView : INotifyPropertyChanged
    {
        public AuthenticationModelView()
        {
            AuthenticationTypeList = new ObservableCollection<AuthenticationType>();
            AuthenticationTypeList.Add(new AuthenticationType() { Id=1, Name="Windows" });
            AuthenticationTypeList.Add(new AuthenticationType() { Id = 2, Name = "Sql авторизація" });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<AuthenticationType> AuthenticationTypeList
        {
            set;
            get;
        }

        int? authenticationType;
        public int? AuthenticationType
        {
            set
            {
                authenticationType = value;
            } 
            get
            {
                return authenticationType;
            }
        }
            

        public AppCommand LoginCommand
        {
            get
            {
                return
                  ( new AppCommand(obj =>
                  {
                      Application.Current.Properties["UserId"] = 1;
                      Application.Current.Properties["EnterpriseId"] = 1;
                      new MainWindow().Show();
                      

                  }));
            }
        }
    }
}
