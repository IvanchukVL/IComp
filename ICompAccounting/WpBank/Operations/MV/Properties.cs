using ICompAccounting.Common;
using ICompAccounting.Model.Entities.oper;
using ICompAccounting.Model.Entities.org;
using ICompAccounting.WpBank.Operations.MV.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ICompAccounting.WpBank
{
    public partial class OperationMV
    {
        public string TitleEditView { set; get; }
        public string ButTextEditView { set; get; }
        public AppCommand CommandEditView { set; get; }
        public ObservableCollection<Operation> OperationsList { get; set; }
        public ObservableCollection<Operation> vPartners { get; set; }

        ObservableCollection<vPartnersAccount> _vPartnersAccounts;
        public ObservableCollection<vPartnersAccount> vPartnersAccounts 
        {
            set
            {
                _vPartnersAccounts = value;
                OnPropertyChanged("vPartnersAccounts");
            }
            get { return _vPartnersAccounts; }
        }
        public vPartnersAccount SelectedPartnerAccount { get; set; }
        public string FindText { get; set; }

        vOperationOut selectedRow;
        public vOperationOut SelectedRow 
        {
            set
            {
                selectedRow = value;
                OnPropertyChanged("SelectedRow");
            }
            get { return selectedRow; }
        }

        vOperationOut row;
        public vOperationOut Row
        {
            set
            {
                row = value;
                OnPropertyChanged("Row");
            }
            get { return row; }
        }
        ObservableCollection<vOperationOut> voperationsOut;
        public ObservableCollection<vOperationOut> OperationsOut
        {
            get { return voperationsOut; }
            set
            {
                voperationsOut = value;
                OnPropertyChanged("OperationsOut");
            }
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
