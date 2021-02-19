using ICompAccounting.Model;
using ICompAccounting.WpBank;
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
using System.Windows.Media;

namespace ICompAccounting.ModelView
{
    public class MainMV : INotifyPropertyChanged,IDataErrorInfo
    {
        Repository db;

        public MainMV()
        {
            db = new Repository(Properties.Resources.AccountingConnection);
            Enterprise = (Enterprise)Application.Current.Properties["Enterprise"];
            MenuList = db.GetMenu(((vUser)Application.Current.Properties["User"]).Id);
            PeriodList = db.GetPeriods();

            MainMenu = new ObservableCollection<MenuItem>();
            SetMenu(0, MainMenu);
            Nodes = new ObservableCollection<Node>();
            Year = Enterprise.Year;
            SetPeriod(Nodes,null, Enterprise.Period);


        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        //public void setent()
        //{
        //    Enterprise.Year = Year;
        //    Enterprise.Period = GetSelectedPeriod().Id;
        //    db.Update<Enterprise>("Enterprises", Enterprise);
        //}

        #region Команди для MV
        private AppCommand openDayOperation;
        public AppCommand OpenDayOperation
        {
            get
            {
                return
                  (openDayOperation = new AppCommand(obj =>
                  {
                      ActiveWindow = new DayOperations(this);
                      PageContent = ActiveWindow.Content;
                  }));
            }
        }

        private AppCommand openListTypeOperations;
        public AppCommand OpenListTypeOperations
        {
            get
            {
                return
                  (openListTypeOperations = new AppCommand(obj =>
                  {
                      ActiveWindow = new ListTypeOperations(this);
                      PageContent = ActiveWindow.Content;
                  }));
            }
        }

        private AppCommand addCommand;
        public AppCommand AddCommand
        {
            get
            {
                return
                  (addCommand = new AppCommand(obj =>
                  {
                      //PageContent = "Page1.xaml";
                  }));
            }
        }

        private AppCommand editCommand;
        public AppCommand EditCommand
        {
            get
            {
                return
                  (editCommand = new AppCommand(obj =>
                  {
                      //PageContent = "Page2.xaml";
                  }));
            }
        }
        #endregion

        #region перелік полів класу

        public string Error
        {
            get { return null; }
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "Year":
                        if (this.Year < 2000 || this.Year > 3000)
                            return "The age must be between 10 and 100";
                        break;
                }

                return string.Empty;
            }
        }

        bool _rbDateFilter = false;
        public bool rbDateFilter
        {
            set
            {
                tbDateFilterEnabled = value;
                if (value)
                    rbPeriodFilter = false;
                _rbDateFilter = value;
                OnPropertyChanged("rbDateFilter");
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
                if (value)
                    rbDateFilter = false;
                _rbPeriodFilter = value;
                OnPropertyChanged("rbPeriodFilter");
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

        bool _tbPeriodFilterEnabled = true;
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

        Window _ActiveWindow;
        public Window ActiveWindow { set; get; }
        public ObservableCollection<Node> Nodes { get; set; }
        public List<vmenu> MenuList { set; get; }
        public List<Period> PeriodList { set; get; }
        public DateTime? Dat1 { set; get; } = DateTime.Now.AddDays(-DateTime.Now.Day+1);
        public DateTime? Dat2 { set; get; } = DateTime.Now.AddMonths(1).AddDays(-DateTime.Now.Day);
        int? _Year;
        public int? Year 
        { 
            set
            {
                if (value < 2000 || value > 3000)
                    throw new ArgumentException("Значення мусть бути менше 3000 і більше 2000!");
                _Year = value;
                OnPropertyChanged("Year");
            }
            get
            {
                return _Year;
            }
        }

        object _PageContent;
        public object PageContent 
        { 
            set
            {
                _PageContent = value;
                OnPropertyChanged("PageContent");

            }
            get
            {
                return _PageContent;
            }
        }

        public Enterprise Enterprise
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


        ObservableCollection<MenuItem> _MainMenu;
        public ObservableCollection<MenuItem> MainMenu
        {
            get { return _MainMenu; }
            set
            {
                _MainMenu = value;
            }
        }
        #endregion

        #region методі класу
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

        private void SetPeriod(ObservableCollection<Node> Collection, Node Parent,  int? SelectedId)
        {
            int? ParentId = Parent == null ? 0 : Parent.Id;
            foreach (Period row in PeriodList.Where(x => x.ParentId == ParentId))
            {
                //PropertyInfo info = null;
                Node node;
                node = new Node(row.Id, row.Code, row.Description);
                if (ParentId == 0)
                    node.IsExpanded = true;

                if (node.Id == SelectedId)
                {
                    if (Parent != null)
                        Parent.IsExpanded = true;
                    node.IsSelected = true;
                }

                Collection.Add(node);
                SetPeriod(node.Nodes, node, SelectedId);
            }
        }


        public Node GetSelectedPeriod()
        {
            return GetSelectedPeriod(Nodes);
        }

        private Node GetSelectedPeriod(ObservableCollection<Node> Periods)
        {
            Node res;
            foreach (Node node in Periods)
            {
                if (node.IsSelected)
                    return node;

                if (node.Nodes != null)
                {
                    res = GetSelectedPeriod(node.Nodes);
                    if (res != null)
                        return res;
                }
            }

            return null;
        }
        #endregion

    }


    /// <summary>
    /// Клас елементів меню
    /// </summary>
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

    /// <summary>
    /// Клас елементів дерева
    /// </summary>
    public class Node
    {
        public Node(int id, string name, string header)
        {
            Id = id;
            Name = name;
            Header = header;
        }

        private ObservableCollection<Node> _Nodes;
        public ObservableCollection<Node> Nodes
        {
            get { return _Nodes ?? (_Nodes = new ObservableCollection<Node>()); }
            set { _Nodes = value; }
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Header { get; set; }
        public bool IsSelected { get; set; }
        public bool IsExpanded { get; set; }
    }

}
