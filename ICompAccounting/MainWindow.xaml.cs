﻿using ICompAccounting.ModelView;
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
            DataContext = new MainMV();
            tbYear.PreviewTextInput += (DataContext as MainMV).NumberValidationTextBox;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Period.Items.Count.ToString());
        }

        private void TreeViewItem_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
    }
}
