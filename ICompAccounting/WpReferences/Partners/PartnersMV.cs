using ICompAccounting.Model;
using ICompAccounting.Model.Entities.org;
using ICompAccounting.ModelView;
using ICompAccounting.UC.ModelView;
using ICompAccounting.WpMain;
using ICompAccounting.WpReferences.Accounts;
using ICompAccounting.WpReferences.Partners;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace ICompAccounting.WpReferences.ModelView
{
    public class PartnersMV : INotifyPropertyChanged, IGridEdition
    {
        public PartnersMV()
        {
            db = new Repository(Properties.Resources.AccountingConnection);
            List<Partner> Orgs = db.GetOrganizations(11);
            Partners = new ObservableCollection<Partner>(Orgs);
            //GridEdition = new GridEditionMV() { Owner = this };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #region властивості класу
        public Partner SelectedRow { get; set; }

        ObservableCollection<Partner> partners;
        public ObservableCollection<Partner> Partners
        {
            get { return partners; }
            set
            {
                partners = value;
                OnPropertyChanged("Organizations");
            }
        }
        public Repository db { get; set; }
        //public GridEditionMV GridEdition { get; set; }
        public EditPartnerView EditOrganization { get; set; }
        public AccountsView AccountsView { get; set; }

        Partner row;
        public Partner Row
        {
            set
            {
                row = value;
                OnPropertyChanged("Row");
            }
            get { return row; }
        }
        public string TitleEditView { set; get; }
        public string ButTextEditView { set; get; }
        public AppCommand CommandEditView { set; get; }
        #endregion

        #region Команди

        public AppCommand OpenWindow
        {
            get
            {
                return
                  (new AppCommand(obj =>
                  {
                      if (SelectedRow == null)
                      {
                          MessageBox.Show("Не виділено жодного запису!");
                          return;
                      }

                      AccountsView = new AccountsView();
                      (AccountsView.DataContext as AccountsMV).Owner = this;
                      AccountsView.ShowDialog();
                  }));
            }
        }

        public AppCommand EditRow
        {
            get
            {
                return
                  (new AppCommand(obj =>
                  {
                      if (SelectedRow == null)
                      {
                          MessageBox.Show("Не виділено жодного запису!");
                          return;
                      }

                      EditOrganization = new EditPartnerView();
                      EditOrganization.DataContext = this;
                      TitleEditView = "Редагування запису";
                      ButTextEditView = "Редагувати";
                      CommandEditView = SaveExistsRow;
                      Row = SelectedRow;
                      EditOrganization.ShowDialog();
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
                      EditOrganization = new EditPartnerView();
                      EditOrganization.DataContext = this;
                      TitleEditView = "Створення нового запису";
                      ButTextEditView = "Створити";
                      Row = new Partner();
                      CommandEditView = SaveNewRow;
                      EditOrganization.ShowDialog();
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
                      db.Insert("Partners", Row);
                      Partners.Add(Row);
                      EditOrganization.Close();
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
                      db.Update("Partners", Row);
                      EditOrganization.Close();
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
                      EditOrganization.Close();
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
                      if (MessageBox.Show("Ви дійсно хочете видалити запис?", "Видалення запису", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                      Row = SelectedRow;
                      db.Delete("Partners", Row);
                      Partners.Remove(Row);
                  }));
            }
        }

        public AppCommand Save
        {
            get
            {
                return
                  (new AppCommand(obj =>
                  {
                      MessageBox.Show("Спроба збереження запису!");
                  }));
            }
        }

        public AppCommand DoubleClick
        {
            get
            {
                return
                  (new AppCommand(obj =>
                  {
                      MessageBox.Show("Подвійний клік!");
                  }));
            }
        }

        #endregion
    }
}
