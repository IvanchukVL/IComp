using ICompAccounting.Model;
using ICompAccounting.ModelView;
using ICompAccounting.UC.ModelView;
using ICompAccounting.WpMain;
using ICompAccounting.WpReferences.ModelView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace ICompAccounting.WpReferences.Accounts
{
    public class AccountsMV : INotifyPropertyChanged,IGridEdition
    {

        public AccountsMV()
        {
            DbOrg = new RepositoryOrg(Properties.Resources.BD_ORGConnection);
            GridEdition = new GridEditionMV();
            //List<Account> Acc = DbOrg.GetAccounts(11);
            //Accounts = new ObservableCollection<Account>(Acc);
        }

        public string TitleAccountView { set; get; }

        PartnersMV owner;
       public PartnersMV Owner
        {
            get { return owner; }
            set
            {
                owner = value;
                List<Account> Acc = DbOrg.GetAccounts(owner.SelectedRow.KOD);
                TitleAccountView = $"Перелік розрахункових рахунків для {owner.SelectedRow.NAZVA_ORG}";
                Accounts = new ObservableCollection<Account>(Acc);
                OnPropertyChanged("Accounts");
            }
        }

        public GridEditionMV GridEdition { get; set; }
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

        public RepositoryOrg DbOrg { get; set; }
        Account selectedRow;
        public Account SelectedRow
        {
            set
            {
                selectedRow = value;
                OnPropertyChanged("Row");
            }
            get { return selectedRow; }
        }

        public EditAccountView EditAccount { set; get; }


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
                      //TitleEditView = "Редагування запису";
                      //ButTextEditView = "Редагувати";
                      //CommandEditView = SaveExistsRow;
                      //Row = SelectedRow;
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
                      //EditOrganization = new EditPartnerView();
                      //EditOrganization.DataContext = this;
                      //TitleEditView = "Створення нового запису";
                      //ButTextEditView = "Створити";
                      //Row = new Partner();
                      //CommandEditView = SaveNewRow;
                      //EditOrganization.ShowDialog();
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
                      //DbOrg.Insert("BD_ORG", Row);
                      //Partners.Add(Row);
                      //EditOrganization.Close();
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
                      //DbOrg.Update("BD_ORG", Row);
                      //EditOrganization.Close();
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
