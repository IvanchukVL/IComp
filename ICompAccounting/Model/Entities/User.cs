using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ICompAccounting.Model.Entities
{
    public class vUser
    {
        [Key]
        public int Id { set; get; }
        public string Login { set; get; }
        public string PIB { set; get; }
    }
}
