using ICompAccounting.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ICompAccounting.ModelView
{
    public class MainMV : INotifyPropertyChanged
    {
        Repository db;
        //List<vmenu> _MenuList;
        //vEnterprise _Enterprise;
        ObservableCollection<MenuItem> _MainMenu;

        public MainMV()
        {
            db = new Repository(Properties.Resources.AccountingConnection);
            Enterprise = (vEnterprise)Application.Current.Properties["Enterprise"];
            MenuList = db.GetMenu(((vUser)Application.Current.Properties["User"]).Id);
            PeriodList = db.GetPeriods();

            MainMenu = new ObservableCollection<MenuItem>();
            SetMenu(0, MainMenu);
            Nodes = new ObservableCollection<Node>();
            SetPeriod(0, Nodes);


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

        bool _rbDateFilter = false;
        public bool rbDateFilter
        {
            set
            {
                tbDateFilterEnabled = value;
                _rbDateFilter = value;
                OnPropertyChanged("rbDatesFilter");
            }

            get
            {
                return _rbDateFilter;
            }
        }

        bool _rbPeriodFilter = false;
        public bool rbPeriodFilter
        {
            set
            {
                tbPeriodFilterEnabled = value;
                _rbPeriodFilter = value;
                OnPropertyChanged("rbDatesFilter");
            }

            get
            {
                return _rbPeriodFilter;
            }
        }


        bool _tbDatesFilterEnabled;
        public bool tbDateFilterEnabled
        {
            set
            {
                _tbDatesFilterEnabled = value;
                OnPropertyChanged("tbDateFilterEnabled");
            }
            get
            {
                return _tbDatesFilterEnabled;
            }
        }

        bool _tbPeriodFilterEnabled;
        public bool tbPeriodFilterEnabled
        {
            set
            {
                _tbPeriodFilterEnabled = value;
                OnPropertyChanged("tbPeriodFilterEnabled");
            }
            get
            {
                return _tbPeriodFilterEnabled;
            }
        }

        public ObservableCollection<Node> Nodes { get; set; }
        public List<vmenu> MenuList { set; get; }
        public List<Period> PeriodList { set; get; }

        public vEnterprise Enterprise
        {
            set;
            get;
        }

        public string Title
        {
            get
            {
                return $"{Enterprise.Name} {Enterprise.Account} {((vUser)Application.Current.Properties["User"]).PIB}";
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

        public void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
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
                    CurMenu = new MenuItem(row.Name, (AppCommand)info.GetValue(this, null))
                    {
                        Bold = row.Bold
                    };
                }
                else
                    CurMenu = new MenuItem(row.Name)
                    {
                        Bold = row.Bold
                    };

                Collection.Add(CurMenu);
                SetMenu(row.MenuId, CurMenu.Items);
            }
        }

        private void SetPeriod(int? ParentId, ObservableCollection<Node> Collection)
        {
            foreach (Period row in PeriodList.Where(x => x.ParentId == ParentId))
            {
                PropertyInfo info = null;
                Node node;
                node = new Node(row.Code, row.Description);

                Collection.Add(node);
                SetPeriod(row.Id, node.Nodes);
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
        public bool Bold { get; set; }
        public string FontWeight { get { return Bold == true ? "Bold" : "Normal"; } }

    }

    public class Node
    {
        public Node(string name, string header)
        {
            Header = header;
            Name = name;
        }

        private ObservableCollection<Node> _Nodes;
        public ObservableCollection<Node> Nodes
        {
            get { return _Nodes ?? (_Nodes = new ObservableCollection<Node>()); }
            set { _Nodes = value; }
        }

        public string Name { get; set; }
        public string Header { get; set; }
    }

}
