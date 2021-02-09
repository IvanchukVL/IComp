using ICompAccounting.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ICompAccounting.ModelView
{
    public class AuthenticationMV : INotifyPropertyChanged
    {
        Repository db;
        public ObservableCollection<vReferenceValue> AuthenticationModeList { set; get; }
        public string AuthenticationMode { set; get; } = "1";
        public List<vEnterprise> Enterprises { set; get; }
        public vEnterprise SelectedEnterprise { set; get; }
        public AuthenticationMV()
        {
            db = new Repository(Properties.Resources.AccountingConnection);
            AuthenticationModeList = new ObservableCollection<vReferenceValue>(db.GetReferenceValues("AuthenticationMode"));
            Enterprises = db.GetEnterprises();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public Result LoginCommand()
        {
            if (SelectedEnterprise == null)
                return new Result(false, "1", "Не вибрано підприємство!");

            if (AuthenticationMode == null)
                return new Result(false, "1", "Не вибрано спосіб аутентифікації!");

            //Визначення користувача

            Application.Current.Properties["User"] = db.GetUser("VIvanchuk"); 
            Application.Current.Properties["Enterprise"] = SelectedEnterprise;
            new MainWindow().Show();
            MessageBox.Show($"Команда {AuthenticationMode}");
            return new Result(true);
        }

        //public AppCommand LoginCommand
        //{
        //    get
        //    {
        //        return
        //          ( new AppCommand(obj =>
        //          {
        //              Application.Current.Properties["UserId"] = 1;
        //              Application.Current.Properties["EnterpriseId"] = 1;
        //              new MainWindow().Show();
        //              MessageBox.Show($"Команда {AuthenticationMode}");
        //          }));
        //    }
        //}
    }

}
