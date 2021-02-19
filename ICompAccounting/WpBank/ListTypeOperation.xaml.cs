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
using System.Windows.Shapes;

namespace ICompAccounting.WpBank
{
    /// <summary>
    /// Interaction logic for ListOperations.xaml
    /// </summary>
    public partial class ListTypeOperations : Window
    {
        public MainMV OwnerMV { set; get; }

        public ListTypeOperations(MainMV ownerMV = null)
        {
            OwnerMV = ownerMV;
            InitializeComponent();
        }
    }
}
