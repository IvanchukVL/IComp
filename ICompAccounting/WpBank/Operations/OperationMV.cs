using ICompAccounting.Common;
using ICompAccounting.Model;
using ICompAccounting.Model.Entities;
using ICompAccounting.Model.Entities.oper;
using ICompAccounting.Model.Entities.org;
using ICompAccounting.WpBank.Operations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace ICompAccounting.WpBank
{
    public partial class OperationMV: INotifyPropertyChanged,IDataErrorInfo
    {
        public Repository db { set; get; }
        public EditOperationView EditWindow { get; set; }
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


        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Row.Purpose":
                        if (string.IsNullOrEmpty(Row.Purpose))
                        {
                            error = "Призначення може містити від 0 до 250 символів!";
                        }
                        break;
                    case "Name":
                        //Обработка ошибок для свойства Name
                        break;
                    case "Position":
                        //Обработка ошибок для свойства Position
                        break;
                }
                return error;
            }
        }

        public string Error => string.Empty;

    }
}
