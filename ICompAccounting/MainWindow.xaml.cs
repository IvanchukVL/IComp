using AccountingContext.dl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ICompAccounting
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<vmenu> MenuList;
        Repository db;
        public MainWindow()
        {
            InitializeComponent();
            db = new Repository(Properties.Resources.AccountingConnection);
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MenuList = await db.GetMenu(1);
            SetMenu(0, cMenu.Items);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void SetMenu(int? ParentId, ItemCollection Collection)
        {
            foreach (vmenu row in MenuList.Where(x => x.ParentId == ParentId))
            {
                int index = Collection.Add(new MenuItem() { Header = row.Name });
                MenuItem item = (MenuItem)Collection[index];
                SetMenu(row.MenuId, item.Items);
            }
        }


    }
}
