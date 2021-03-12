using ICompAccounting.Model;
using ICompAccounting.Model.Entities;
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
            DataContext = new AuthenticationMV();
            this.Enterprices.Focus();
        }

        private void ButLog_Click(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;
            Result Res = ((AuthenticationMV)DataContext).LoginCommand();
            Cursor = Cursors.Arrow;
            if (Res.Res == true)
                Close();
            else
                MessageBox.Show(Res.Message);
        }
    }
}
