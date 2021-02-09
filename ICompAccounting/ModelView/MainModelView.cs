﻿using ICompAccounting.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ICompAccounting.ModelView
{
    public class MainModelView : INotifyPropertyChanged
    {
        Repository db;
        List<vmenu> _MenuList;
        Enterprise _Enterprise;
        ObservableCollection<MenuItem> _MainMenu;

        public MainModelView()
        {
            db = new Repository(Properties.Resources.AccountingConnection);
            _Enterprise = db.GetEnterprise((int?)Application.Current.Properties["EnterpriseId"]);
            _MenuList = db.GetMenu((int?)Application.Current.Properties["UserId"]);

            _MainMenu = new ObservableCollection<MenuItem>();
            SetMenu(0, _MainMenu);
        }

        private AppCommand addCommand;
        public AppCommand AddCommand
        {
            get
            {
                return
                  (addCommand = new AppCommand(obj =>
                  {
                      MessageBox.Show("Запуск команди!");
                  }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<vmenu> MenuList
        {
            get
            {
                return _MenuList;
            }
        }

        public Enterprise Enterprise
        {
            get
            {
                return _Enterprise;
            }
        }

        public string Title
        {
            get
            {
                return $"{_Enterprise.Name} {_Enterprise.Account}";
            }
        }


        public ObservableCollection<MenuItem> MainMenu
        {
            get { return _MainMenu; }
            set
            {
                _MainMenu = value;
            }
        }

        private void SetMenu(int? ParentId, ObservableCollection<MenuItem> Collection)
        {
            foreach (vmenu row in MenuList.Where(x => x.ParentId == ParentId))
            {
                PropertyInfo info = null;
                MenuItem CurMenu;
                if (!string.IsNullOrEmpty(row.Command))
                {
                    info = GetType().GetProperty(row.Command);
                    CurMenu = new MenuItem(row.Name, (AppCommand)info.GetValue(this, null));
                }
                else
                    CurMenu = new MenuItem(row.Name);

                Collection.Add(CurMenu);
                SetMenu(row.MenuId, CurMenu.Items);
            }
        }
    }


    public class MenuItem
    {
        private ObservableCollection<MenuItem> _Items;

        public ObservableCollection<MenuItem> Items
        {
            get { return _Items ?? (_Items = new ObservableCollection<MenuItem>()); }
            set { _Items = value; }
        }

        public MenuItem(string header, ICommand command)
        {
            Header = header;
            Command = command;
        }

        public MenuItem(string header)
        {
            Header = header;
        }

        public MenuItem()
        {

        }

        public string Header { get; set; }

        public ICommand Command { get; set; }
        public string CommandName { get; set; }
        public object Icon { get; set; }
        public bool IsCheckable { get; set; }
        private bool _IsChecked;
        public bool IsChecked
        {
            get { return _IsChecked; }
            set
            {
                _IsChecked = value;
            }
        }

        public bool Visible { get; set; }
        public bool IsSeparator { get; set; }
        public string InputGestureText { get; set; }
        public string ToolTip { get; set; }
        public int MenuHierarchyID { get; set; }
        public int ParentMenuHierarchyID { get; set; }
        public string IconPath { get; set; }
        public bool IsAdminOnly { get; set; }
        public object Context { get; set; }
        public int int_Sequence { get; set; }
        public int int_KeyIndex { get; set; }
    }

}
