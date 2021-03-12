using ICompAccounting.Model.Entities.org;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int? OperationId { set; get; }
        public string Purpose { set; get; }
        public bool? Exported { set; get; }
    }

    public class vOperationOut: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
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

        OperationOut operationOut;
        public OperationOut OperationOut
        {
            set
            {
                operationOut = value;
                OnPropertyChanged("OperationOut");
            }
            get
            {
                return operationOut;
            }
        }
    }
}
