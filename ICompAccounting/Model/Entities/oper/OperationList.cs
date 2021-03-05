using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ICompAccounting.Model.Entities.oper
{
    [Table("OperationList", Schema = "oper")]
    public class Operation
    {
        [Key]
        public int? Id { set; get; }
        public string Code { set; get; }
        public string Description { set; get; }
        public int? Status { set; get; }
    }
}
