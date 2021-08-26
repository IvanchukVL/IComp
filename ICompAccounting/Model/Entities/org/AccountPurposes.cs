using ICompAccounting.Model.Entities.oper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ICompAccounting.Model.Entities.org
{
    [Table("AccountPurposes", Schema = "org")]
    public class AccountPurpose
    {
        [Key]
        public int? Id { set; get; }
        public int? AccountId { set; get; }
        public int? OperationId { set; get; }
        public string Purpose { set; get; }
        public int? Status { set; get; } = 1;

    }

    [Table("vAccountsPurposes", Schema = "dbo")]
    public class vAccountsPurposes
    {
        [Key]
        public int? AccountId { set; get; }
        public string Description { set; get; }
        public string IBAN { set; get; }
        public string Purpose { set; get; }
        public string OperationText { set; get; }

    }

    [Table("vPurposes", Schema = "dbo")]
    public class vPurposes
    {
        [Key]
        public int? Id { set; get; }
        public int? AccountId { set; get; }
        public int? OperationId { set; get; }
        public string Description { set; get; }

    }

}
