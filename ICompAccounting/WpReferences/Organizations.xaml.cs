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

namespace ICompAccounting.WpReferences
{
    /// <summary>
    /// Interaction logic for Organizations.xaml
    /// </summary>
    public partial class Organizations : Window
    {
        public MainMV OwnerMV { set; get; }
        public Organizations(MainMV ownerMV = null)
        {
            OwnerMV = ownerMV;
            InitializeComponent();

            var size = this.Height;
        }
    }
}
