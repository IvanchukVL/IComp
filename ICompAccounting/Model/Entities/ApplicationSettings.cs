using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace ICompAccounting.Model.Entities
{
    public static class AppSettings
    {
        public static string AccountingConnection
        { 
            get
            {
                return ConfigurationManager.AppSettings["AccountingConnection"].Replace("@user", Properties.Resources.Login).Replace("@pass", Properties.Resources.Password);

            }
        }
    }
}
