using ICompAccounting.Common;
using ICompAccounting.Model.Entities.oper;
using ICompAccounting.Model.Entities.org;
using ICompAccounting.WpBank.Operations;
using ICompAccounting.WpBank.Operations.MV.Entities;
using ICompAccounting.WpBank.Operations.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace ICompAccounting.WpBank
{
    public partial class OperationMV
    {
        /// <summary>
        /// Кнопка нового запису
        /// </summary>
        public AppCommand NewRow
        {
            get
            {
                return
                  (new AppCommand(obj =>
                  {
                      EditWindow = new EditOperationView();
                      EditWindow.DataContext = this;
                      TitleEditView = "Створення нового запису";
                      ButTextEditView = "Створити";
                      Row = new vOperationOut()
                      {
                          BankId = 1,
                          Exported = false,
                          OperDat = 232132
                      };
                      CommandEditView = SaveNewRow;
                      EditWindow.ShowDialog();
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

                      EditWindow = new EditOperationView();
                      EditWindow.DataContext = this;
                      TitleEditView = "Редагування запису";
                      ButTextEditView = "Редагувати";
                      CommandEditView = SaveExistsRow;
                      Row = (vOperationOut)SelectedRow.Clone();
                      //Row = SelectedRow;
                      Row.SetRefferencess();
                      //Row.Accounts = new ObservableCollection<Account>(db.GetAccounts(Row.PartnerId));
                      //Row.vPurposes = new ObservableCollection<vPurposes>(db.GetPurposes(Row.AccountId).ToList());
                      EditWindow.ShowDialog();
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
                      Row.IsValidating = true;
                      Row.Validate();
                      if (Row.IsValid)
                      {
                          db.Insert("OperationsOut", (OperationOut)Row);
                          OperationsOut.Add(Row);
                          EditWindow.Close();
                      }
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
                      Row.IsValidating = true;
                      Row.Validate();
                      if (Row.IsValid)
                      {
                          db.Update("OperationsOut", (OperationOut)Row);
                          SelectedRow.SetValues(Row);
                          EditWindow.Close();
                      }
                  }));
            }
        }

        public AppCommand RefreshRow
        {
            get
            {
                return
                  (new AppCommand(obj =>
                  {
                      SelectedRow.Partner = db.GetPartner(Convert.ToInt32(SelectedRow.PartnerId));
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
                      //Row = SelectedRow;
                      //selectedRow.PartnerId = 8;
                      EditWindow.Close();
                  }));
            }
        }

        public AppCommand KeyIns
        {
            get
            {
                return
                  (new AppCommand(obj =>
                  {
                      if ((obj as KeyEventArgs).Key == Key.Insert)
                      {
                          NewRow.Execute();
                      }
                  }));
            }
        }

        /// <summary>
        /// Подвійний клік мишкою на видаткових операціях
        /// </summary>
        //public AppCommand DoubleClick
        //{
        //    get
        //    {
        //        return
        //          (new AppCommand(obj =>
        //          {
        //              //var ddd = (MouseButtonEventArgs)obj;

        //              if (SelectedRow == null)
        //              {
        //                  MessageBox.Show("Не виділено жодного запису!");
        //                  return;
        //              }

        //              EditWindow = new EditOperationView();
        //              EditWindow.DataContext = this;
        //              TitleEditView = "Редагування запису";
        //              ButTextEditView = "Редагувати";
        //              CommandEditView = SaveExistsRow;
        //              Row = (vOperationOut)SelectedRow.Clone();
        //              EditWindow.ShowDialog();
        //          }));
        //    }
        //}

        /// <summary>
        /// Видалення запису
        /// </summary>
        public AppCommand DeleteRow
        {
            get
            {
                return
                  (new AppCommand(obj =>
                  {
                      if (MessageBox.Show("Ви дійсно хочете видалити запис?", "Видалення запису", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                      {
                          Row = SelectedRow;
                          db.Delete("OperationsOut", (OperationOut)Row);
                          OperationsOut.Remove(Row);
                      }
                  }));
            }
        }

        public AppCommand OpenFindAccount
        {
            get
            {
                return
                  (new AppCommand(obj =>
                  {
                      FindAccountWindow = new FindAccount();
                      vPartnersAccounts = new ObservableCollection<vPartnersAccount>(db.GetPartnerAccounts(""));
                      FindAccountWindow.DataContext = this;
                      FindAccountWindow.ShowDialog();
                  }));
            }
        }

        public AppCommand FindAccount
        {
            get
            {
                return
                  (new AppCommand(obj =>
                  {
                      vPartnersAccounts = new ObservableCollection<vPartnersAccount>(db.GetPartnerAccounts(FindText));
                      //vPartnersAccounts.CollectionChanged();
                  }));
            }
        }

        public AppCommand InsertValues
        {
            get
            {
                return
                  (new AppCommand(obj =>
                  {
                      Row.PartnerId = SelectedPartnerAccount.KOD;
                      Row.AccountId = SelectedPartnerAccount.AccountId;
                      FindAccountWindow.Close();
                  }));
            }
        }




    }
}
