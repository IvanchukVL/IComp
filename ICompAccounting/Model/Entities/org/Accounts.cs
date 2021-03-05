using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ICompAccounting.Model.Entities.org
{
    [Table("Accounts", Schema = "org")]
    public class Account
    {
        [Key]
        public int? Id { set; get; }
        public int? PartnerId { set; get; }
        public string IBAN { set; get; }
        public string MFO { set; get; }
        public int? Status { set; get; } = 1;
    }
}
