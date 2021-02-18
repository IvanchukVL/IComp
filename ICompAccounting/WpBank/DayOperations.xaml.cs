using ICompAccounting.ModelView;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ICompAccounting.WpBank
{
    /// <summary>
    /// Interaction logic for DayOperation.xaml
    /// </summary>
    public partial class DayOperations : Window
    {
        public MainMV OwnerMV { set; get; }
        public DayOperations(MainMV ownerMV = null)
        {
            OwnerMV = ownerMV;
            InitializeComponent();

            var ddd = this.OwnerMV;
        }
    }
}
