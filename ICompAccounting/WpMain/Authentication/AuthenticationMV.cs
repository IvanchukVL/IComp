using ICompAccounting.Common;
using ICompAccounting.Model;
using ICompAccounting.Model.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace ICompAccounting.ModelView
{
    public class AuthenticationMV : INotifyPropertyChanged
    {
        Repository db;
        public ObservableCollection<vReferenceValue> AuthenticationModeList { set; get; }
        public string AuthenticationMode { set; get; } = "1";
        public List<Enterprise> Enterprises { set; get; }
        public Enterprise SelectedEnterprise { set; get; }
        public AuthenticationMV()
        {
            db = new Repository(AppSettings.AccountingConnection);
            AuthenticationModeList = new ObservableCollection<vReferenceValue>(db.GetReferenceValues("AuthenticationMode"));
            Enterprises = db.GetEnterprises();
            SelectedEnterprise = Enterprises[0];
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Аутентифікація користувача
        /// </summary>
        /// <returns></returns>
        public Result LoginCommand()
        {
            if (SelectedEnterprise == null)
                return new Result(false, "1", "Не вибрано підприємства!");

            if (AuthenticationMode == null)
                return new Result(false, "1", "Не вибрано спосіб аутентифікації!");

            //Визначення користувача
            IPrincipal principal = Thread.CurrentPrincipal;
            IIdentity identity = principal.Identity;
            if (!identity.IsAuthenticated)
                return new Result(false) { Message = "Користувач не аутентифікований!" };



            vUser DbUser = db.GetUser(identity.Name);
            if (DbUser != null)
            {
                Application.Current.Properties["User"] = DbUser;
                Application.Current.Properties["Enterprise"] = SelectedEnterprise;
                new MainWindow().Show();
                return new Result(true);
            }
            else
                return new Result(false) { Message = "Користувач не зареєстрований в програмному комплексі!" };

        }

    }

}
