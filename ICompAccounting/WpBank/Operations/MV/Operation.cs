using ICompAccounting.Common;
using ICompAccounting.Model;
using ICompAccounting.Model.Entities;
using ICompAccounting.Model.Entities.oper;
using ICompAccounting.Model.Entities.org;
using ICompAccounting.WpBank.Operations;
using ICompAccounting.WpBank.Operations.MV.Entities;
using ICompAccounting.WpBank.Operations.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace ICompAccounting.WpBank
{
    public partial class OperationMV: INotifyPropertyChanged
    {
        public Repository db { set; get; }
        public EditOperationView EditWindow { get; set; }
        public FindAccount FindAccountWindow { get; set; }
        public ObservableCollection<vAccountsPurposes> vAccountsPurposes { set; get; }

        public OperationMV()
        {
            db = new Repository(AppSettings.AccountingConnection);
            OperationsOut = new ObservableCollection<vOperationOut>(db.OperationsOut(1, DateTime.Now));
            OperationsList = new ObservableCollection<Operation>(db.GetOperationList());
            //Accounts = new ObservableCollection<Account>(db.GetAccounts(8));

        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
