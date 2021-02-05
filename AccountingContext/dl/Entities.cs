using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AccountingContext.dl
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
    }

    public class User
    {
        [Key]
        public string Login { set; get; }
        public string Name { set; get; }
    }

    public class menu
    {
        [Key]
        public int? Id { set; get; }
        public string Name { set; get; }
        public string Command { set; get; }
        public int? ParentId { set; get; }
        public int? Status { set; get; }
    }
}
