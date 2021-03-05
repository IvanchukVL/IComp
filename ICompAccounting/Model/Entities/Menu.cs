using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ICompAccounting.Model.Entities
{
    public class vmenu
    {
        [Key]
        public int? UserId { set; get; }
        [Key]
        public int? MenuId { set; get; }
        public string Name { set; get; }
        public string Command { set; get; }
        public int? ParentId { set; get; }
        public bool Bold { set; get; }
    }
}
