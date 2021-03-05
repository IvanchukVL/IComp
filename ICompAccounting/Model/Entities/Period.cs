using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ICompAccounting.Model.Entities
{
    public class Period
    {
        [Key]
        public int Id { set; get; }
        public string Code { set; get; }
        public string Description { set; get; }
        public int? ParentId { set; get; }
        public int? Status { set; get; }
    }
}
