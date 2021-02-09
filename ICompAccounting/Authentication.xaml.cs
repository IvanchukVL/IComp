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

namespace ICompAccounting
{
    /// <summary>
    /// Interaction logic for Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
            DataContext = new AuthenticationModelView();

        }

        private void ButLog_Click(object sender, RoutedEventArgs e)
        {
            ((AuthenticationModelView)DataContext).LoginCommand.Execute(null);
            this.Close();
        }
    }
}
