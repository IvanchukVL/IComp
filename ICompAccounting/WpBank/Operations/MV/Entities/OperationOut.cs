using ICompAccounting.Model;
using ICompAccounting.Model.Entities;
using ICompAccounting.Model.Entities.oper;
using ICompAccounting.Model.Entities.org;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ICompAccounting.WpBank.Operations.MV.Entities
{
    public class vOperationOut : OperationOut, INotifyPropertyChanged, IDataErrorInfo
    {
        Repository db { set; get; }
        public bool IsValidating = false;
        private static readonly string[] ValidatedProperties = { "Purpose", "Amount", "PartnerId", "AccountId", "Position" };
        public bool IsValid
        {
            get
            {
                foreach (string property in ValidatedProperties)
                {
                    if (!string.IsNullOrEmpty(GetValidationError(property)))
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        private string GetValidationError(string columnName)
        {
            string result = String.Empty;
            if (!IsValidating)
                return result;

            switch (columnName)
            {
                case "Purpose":
                    if (string.IsNullOrEmpty(Purpose))
                        result = "Призначення може містити від 0 до 250 символів!";
                    break;
                case "Amount":
                    if (Amount == null || Amount == 0)
                        result = "Сума не може містити пусте або нульове значення!";
                    break;
                case "PartnerId":
                    if (PartnerId == null)
                        result = "Не вказано Id партнера!";
                    break;
                case "AccountId":
                    if (AccountId == null)
                        result = "Не вказано рахунку партнера!";
                    break;
                case "Position":

                    break;
            }

            return result;
        }

        public string this[string columnName]
        {
            get
            {
                return GetValidationError(columnName);
            }
        }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public void Validate()
        {
            foreach (string property in ValidatedProperties)
            {
                OnPropertyChanged(property);
            }
        }

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
                if (PropertyChanged != null && value != null)
                {
                    Purpose = vPurposes.FirstOrDefault(x => x.Id == value).PurposeTemplate;
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

        public new decimal? Amount
        {
            set
            {
                base.Amount = value;
                //OnPropertyChanged("Amount");
            }
            get
            {
                return base.Amount;
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
