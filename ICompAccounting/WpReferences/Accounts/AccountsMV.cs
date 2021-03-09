using ICompAccounting.Model;
using ICompAccounting.Model.Entities.oper;
using ICompAccounting.Model.Entities.org;
using ICompAccounting.ModelView;
using ICompAccounting.UC.ModelView;
using ICompAccounting.WpMain;
using ICompAccounting.WpReferences.ModelView;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace ICompAccounting.WpReferences.Accounts
{
    public class AccountsMV : INotifyPropertyChanged,IGridEdition111
    {

        public AccountsMV()
        {
            db = new Repository(Properties.Resources.AccountingConnection);
            //GridEdition = new GridEditionMV() { Owner = this };
            var list = db.GetOperationList();
            OperationList = new ObservableCollection<Operation>(list);
            //List<Account> Acc = DbOrg.GetAccounts(11);
            //Accounts = new ObservableCollection<Account>(Acc);
        }

        public string TitleAccountView { set; get; }
        public string TitleEditView { set; get; }
        public string ButTextEditView { set; get; }
        public AppCommand CommandEditView { set; get; }

        PartnersMV owner;
       public PartnersMV Owner
        {
            get { return owner; }
            set
            {
                owner = value;
                List<Account> Acc = db.GetAccounts(owner.SelectedRow.KOD);
                TitleAccountView = $"Перелік розрахункових рахунків для {owner.SelectedRow.NAZVA_ORG}";
                Accounts = new ObservableCollection<Account>(Acc);
                OnPropertyChanged("Accounts");
            }
        }

        //public GridEditionMV GridEdition { get; set; }
        ObservableCollection<Account> accounts;
        public ObservableCollection<Account> Accounts
        {
            get { return accounts; }
            set
            {
                accounts = value;
                OnPropertyChanged("Accounts");
            }
        }

        public Repository db { get; set; }

        Account selectedRow;
        public Account SelectedRow
        {
            set
            {
                selectedRow = value;
                OnPropertyChanged("SelectedRow");
            }
            get { return selectedRow; }
        }

        Account row;
        public Account Row
        {
            set
            {
                row = value;
                OnPropertyChanged("Row");
            }
            get { return row; }
        }

        public EditAccountView EditAccount { set; get; }
        public ObservableCollection<AccountPurpose> accountPurposes;
        public ObservableCollection<AccountPurpose> AccountPurposes 
        {
            set 
            { 
                accountPurposes = value; 
                OnPropertyChanged("AccountPurposes"); 
            }
            get { return accountPurposes; }
        }
        public Operation CurOperation { get { return OperationList[0]; } }


        public ObservableCollection<Operation> OperationList { set; get; }


        public AppCommand EditRow
        {
            get
            {
                return
                  (new AppCommand(obj =>
                  {
                      if (SelectedRow == null)
                      {
                          MessageBox.Show("Не вибрано жодного запису!");
                          return;
                      }

                      EditAccount = new EditAccountView();
                      EditAccount.DataContext = this;
                      TitleEditView = "Редагування запису";
                      ButTextEditView = "Редагувати";
                      CommandEditView = SaveExistsRow;
                      Row = SelectedRow;

                      db.Open();
                      AccountPurposes = db.GetAccountPurposes(SelectedRow.Id).Local.ToObservableCollection();
                      EditAccount.ShowDialog();
                  }));
            }
        }
        public AppCommand NewRow
        {
            get
            {
                return
                  (new AppCommand(obj =>
                  {
                      EditAccount = new EditAccountView();
                      EditAccount.DataContext = this;
                      TitleEditView = "Створення нового запису";
                      ButTextEditView = "Створити";
                      Row = new Account() { PartnerId = Owner.SelectedRow.KOD };
                      CommandEditView = SaveNewRow;
                      EditAccount.ShowDialog();
                  }));
            }
        }

        public AppCommand OpenWindow
        {
            get
            {
                return
                  (new AppCommand(obj =>
                  {
                  }));
            }
        }

        public AppCommand SaveNewRow
        {
            get
            {
                return
                  (new AppCommand(obj =>
                  {
                      db.Insert("Accounts", Row);
                      Accounts.Add(Row);
                      EditAccount.Close();
                  }));
            }
        }

        public AppCommand SaveExistsRow
        {
            get
            {
                return
                  (new AppCommand(obj =>
                  {
                      AccountPurposes.Where(x => x.AccountId == null).Select(x =>{ x.AccountId = SelectedRow.Id; return x; }).ToList();
                      db.dc.SaveChanges();
                      EditAccount.Close();
                  }));
            }
        }

        public AppCommand CancelRow
        {
            get
            {
                return
                  (new AppCommand(obj =>
                  {
                      //EditOrganization.Close();
                  }));
            }
        }

        public AppCommand DeleteRow
        {
            get
            {
                return
                  (new AppCommand(obj =>
                  {
                      //if (MessageBox.Show("Ви дійсно хочете видалити запис?", "Видалення запису", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                      //    Row = SelectedRow;
                      //DbOrg.Delete("BD_ORG", Row);
                      //Partners.Remove(Row);
                  }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
