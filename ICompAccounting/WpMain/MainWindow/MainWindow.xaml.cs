using ICompAccounting.Model;
using ICompAccounting.Model.Entities;
using ICompAccounting.ModelView;
using ICompAccounting.WpBank;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        //List<vmenu> MenuList;
        //Repository db;
        //Enterprise ent;
        public MainWindow()
        {
            InitializeComponent();
            //DataContext = new MainMV();


            //tbYear.PreviewTextInput += (DataContext as MainMV).NumberValidationTextBox;
            //Period.Focus();
        }

        private void button_click(object sender, RoutedEventArgs e)
        {
            var mv = (DataContext as MainMV);
            if (mv.LocalValues == null)
            {
                mv.db.Insert("UsersLocalParams", new UsersLocalParam() { UserId = ((vUser)Application.Current.Properties["User"]).Id, EnterpriseId = mv.Enterprise.Id, Year = mv.Year, Period = mv.GetSelectedPeriod().Id });
            }
            else
            {
                mv.LocalValues.Year = mv.Year;
                mv.LocalValues.Period = mv.GetSelectedPeriod().Id;
                mv.db.Update("UsersLocalParams", mv.LocalValues);
            }
            //var list = MainFrame.BackStack;
            //foreach (JournalEntry fruit in list)
            //{
            //    fruit.Name = "Тест";
            //}
            //var d = list.GetEnumerator();
            //(((DataContext as MainMV).ActiveWindow as DayOperations).DataContext as DayOperationsMV).Title="Інші операції";
        }

    }
}
