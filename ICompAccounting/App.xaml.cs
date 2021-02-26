//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows;

namespace ICompAccounting
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {


        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Непередачувана помилка: " + e.Exception.Message, "Помилка додатку", MessageBoxButton.OK, MessageBoxImage.Warning);
            e.Handled = true;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
        }

        //private void ConfigureServices(IServiceCollection services)
        //{

        //    // ...

        //    //services.AddTransient(typeof(MainWindow));
        //}
    }
}
