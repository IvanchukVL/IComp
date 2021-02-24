using ICompAccounting.ModelView;
using ICompAccounting.WpMain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace ICompAccounting.UC.ModelView
{
    public class GridNavigationMV<T>: INotifyPropertyChanged
    {

        public GridNavigationMV(int countPageRows, IQueryable<T> rows, IGridNavigation owner)
        {
            Owner = owner;
            Rows = rows;
            CountPageRows = countPageRows;
            float res = (float)rows.Count() / (float)CountPageRows;
            CountPages = (int)Math.Ceiling(res); //+ rows.Count()%RowsPage>0?1:0;
            CurrentPage = 1;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


        #region Команди
        private AppCommand openDayOperation;
        public AppCommand SaveRows
        {
            get
            {
                return
                  (openDayOperation = new AppCommand(obj =>
                  {
                      MessageBox.Show("Збереження в MV");
                  }));
            }
        }

        public AppCommand Forward
        {
            get
            {
                return
                  (new AppCommand(obj =>
                  {
                      CurrentPage++;
                  }));
            }
        }
        public AppCommand Backward
        {
            get
            {
                return
                  (new AppCommand(obj =>
                  {
                      if (CurrentPage>1)
                        CurrentPage--;
                  }));
            }
        }

        public AppCommand SkipForward
        {
            get
            {
                return
                  (new AppCommand(obj =>
                  {
                      CurrentPage = CountPages;
                  }));
            }
        }

        public AppCommand SkipBackward
        {
            get
            {
                return
                  (new AppCommand(obj =>
                  {
                          CurrentPage = 1;
                  }));
            }
        }

        #endregion



        #region
        public int CountPageRows { set; get; }
        int currentPage;
        public int CurrentPage 
        {
            set 
            { 
                currentPage = value;
                Owner.BindingPage(new ObservableCollection<T>(GetDataPage()));
                OnPropertyChanged("CurrentStatus"); 
            }
            get { return currentPage; } 
        }
        public int CountPages { set; get; }
        public string CurrentStatus { get { return $"{CurrentPage} з {CountPages}"; } }
        public IQueryable<T> Rows { set; get; }
        public IGridNavigation Owner { set; get; }
        public List<T> GetDataPage()
        {
            return Rows.Skip((CurrentPage-1)* CountPageRows).Take(CountPageRows).ToList();
        }
        #endregion


    }
}
