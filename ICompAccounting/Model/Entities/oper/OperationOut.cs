using ICompAccounting.Model.Entities.org;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ICompAccounting.Model.Entities.oper
{
    [Table("OperationsOut", Schema = "oper")]
    public class OperationOut 
    {
        public int? Id { set; get; }
        public int? BankId { set; get; }
        public int? OperDat { set; get; }
        public int? PartnerId { set; get; }
        public decimal? Amount { set; get; }
        public int? AccountId { set; get; }
        public int? OperationId { set; get; }
        public string Purpose { set; get; }
        public bool? Exported { set; get; }
    }

    public class vOperationOut : OperationOut, INotifyPropertyChanged
    {
        Repository db { set; get; }

        List<Account> _Accounts;
        public List<Account> Accounts
        {
            set
            {
                _Accounts = value;
                OnPropertyChanged("Accounts");
            }
            get
            {
                return _Accounts;
            }
        }


        List<vPurposes> _vPurposes;
        public List<vPurposes> vPurposes 
        {
            set
            {
                _vPurposes = value;
                OnPropertyChanged("vPurposes");
            }
            get
            {
                return _vPurposes;
            }
        }

        public vOperationOut()
        {
            db = new Repository(AppSettings.AccountingConnection);
        }
        public void SetRefferencess()
        {
            Accounts = db.GetAccounts(PartnerId).ToList();
            vPurposes = db.GetPurposes(AccountId).ToList();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public vOperationOut Clone()
        {
            var Res = (vOperationOut)MemberwiseClone();
            Res.Accounts = null;
            Res.vPurposes = null;
            return Res;
        }

        public void SetValues(vOperationOut Row)
        {
            var Properties = typeof(vOperationOut).GetProperties();
            var BaseProperties = typeof(OperationOut).GetProperties().ToList();
            foreach (var prop in Properties)
            {
                if (BaseProperties.Any(x => x.Name == prop.Name))
                    prop.SetValue(this, prop.GetValue(Row));
            }
        }

        public new int? PartnerId
        {
            set
            {
                base.PartnerId = value;
                if (PropertyChanged != null)
                {
                    Partner = db.GetPartner((int)value);
                    Accounts = db.GetAccounts((int)value).ToList();
                }
                OnPropertyChanged("PartnerId");
            }
            get
            {
                return base.PartnerId;
            }
        }

        public new int? AccountId
        {
            set
            {
                base.AccountId = value;
                if (PropertyChanged != null)
                {
                    vPurposes = db.GetPurposes(value).ToList();
                }
                OnPropertyChanged("AccountId");
            }
            get
            {
                return base.AccountId;
            }
        }


        public new int? OperationId
        {
            set
            {
                base.OperationId = value;
                if (PropertyChanged != null)
                {
                    Purpose = "Заготовка призначення";
                }
                OnPropertyChanged("OperationId");
            }
            get
            {
                return base.OperationId;
            }
        }

        public new string Purpose
        {
            set
            {
                base.Purpose = value;
                OnPropertyChanged("Purpose");
            }
            get
            {
                return base.Purpose;
            }
        }


        Partner partner;
        public Partner Partner
        {
            set
            {
                partner = value;
                OnPropertyChanged("Partner");
            }
            get
            {
                return partner;
            }
        }

    }
}
