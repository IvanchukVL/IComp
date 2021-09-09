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
}
