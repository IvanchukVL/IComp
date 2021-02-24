using ICompAccounting.Model;
using ICompAccounting.ModelView;
using ICompAccounting.UC.ModelView;
using ICompAccounting.WpMain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace ICompAccounting.WpReferences.ModelView
{
    public class OrganizationMV : INotifyPropertyChanged, IGridEdition
    {
        public OrganizationMV()
        {
            RepositoryOrg DbOrg = new RepositoryOrg(Properties.Resources.BD_ORGConnection);
            List<BD_ORG> Orgs = DbOrg.GetOrganizations(11);
            Organizations = new ObservableCollection<BD_ORG>(Orgs);
            GridEdition = new GridEditionMV() { Owner = this };

            //GridNavigation = new GridNavigationMV<BD_ORG>(50, DbOrg.GetOrganizations(11), this);

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #region властивості класу

        ObservableCollection<BD_ORG> organizations;
        public ObservableCollection<BD_ORG> Organizations 
        { 
            get { return organizations; } 
            set 
            { 
                organizations = value;
                OnPropertyChanged("Organizations");
            }
        }
        //public GridNavigationMV<BD_ORG> GridNavigation { get; set; }
        public GridEditionMV GridEdition { get; set; }

        #endregion


        public void Save()
        {
            MessageBox.Show("Проба");
        }


        //#region Команди
        //private AppCommand openDayOperation;
        //public AppCommand SaveRows
        //{
        //    get
        //    {
        //        return
        //          (openDayOperation = new AppCommand(obj =>
        //          {
        //              MessageBox.Show("Збереження");
        //          }));
        //    }
        //}

        //#endregion
    }
}
