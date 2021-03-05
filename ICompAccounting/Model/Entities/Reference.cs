using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ICompAccounting.Model.Entities
{
    public class vReferenceValue
    {
        [Key]
        public int Id { set; get; }
        public string ReferenceCode { set; get; }
        public string Value { set; get; }
        public string Description { set; get; }
        public DateTime? Dat1 { set; get; }
        public DateTime? Dat2 { set; get; }
        public int? Sort { set; get; }
    }
}
