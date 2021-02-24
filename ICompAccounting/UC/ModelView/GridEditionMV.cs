﻿using ICompAccounting.ModelView;
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
    public class GridEditionMV: INotifyPropertyChanged
    {

        public GridEditionMV()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


        #region Команди
        public AppCommand SaveRows
        {
            get
            {
                return
                  (new AppCommand(obj =>
                  {
                      Owner.Save();
                  }));
            }
        }
        #endregion


        #region Властивості 
        public IGridEdition Owner { set; get; }

        #endregion
    }
}
