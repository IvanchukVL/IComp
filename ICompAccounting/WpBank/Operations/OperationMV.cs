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
    public class OperationMV: INotifyPropertyChanged
    {
        public OperationMV()
        {
            db = new Repository(AppSettings.AccountingConnection);
            OperationsOut = new ObservableCollection<vOperationOut>(db.OperationsOut(1, DateTime.Now));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


        #region властивості
        public Repository db { set; get; }
        public EditOperationView EditWindow { get; set; }
        public string TitleEditView { set; get; }
        public string ButTextEditView { set; get; }
        public AppCommand CommandEditView { set; get; }
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


        //public vOperationOut
        //{
        //    Partner partner;
        //    public Partner Partner
        //    {
        //        set
        //        {
        //            partner = value;
        //            OnPropertyChanged("Partner");
        //        }
        //        get
        //        {
        //            return partner;
        //        }
        //    }

        //    OperationOut operationOut;
        //    public OperationOut OperationOut
        //    {
        //        set
        //        {
        //            operationOut = value;
        //            OnPropertyChanged("OperationOut");
        //        }
        //        get
        //        {
        //            return operationOut;
        //        }
        //    }
        //}



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
        #endregion

        #region команди
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
                      Row = SelectedRow;
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
                      //db.Insert("Partners", Row);
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
                      db.Update("OperationsOut", (OperationOut)Row);
                      //Row.Partner = db.GetPartner((int)Row.OperationOut.PartnerId);
                      EditWindow.Close();
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
                      EditWindow.Close();
                  }));
            }
        }

        #endregion

    }
}
