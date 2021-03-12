using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ICompAccounting.Model.Entities
{
    [Table("UsersLocalParams", Schema = "dbo")]
    public class UsersLocalParam
    {
        [Key]
        public int? UserId { set; get; }
        [Key]
        public int? EnterpriseId { set; get; }
        public int? Year { set; get; }
        public int? Period { set; get; }
    }
}
