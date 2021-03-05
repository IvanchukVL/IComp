using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ICompAccounting.Model.Entities
{
    public class Enterprise
    {
        [Key]
        public int? Id { set; get; }
        public string Name { set; get; }
        public string Account { set; get; }
        public string MFO { set; get; }
        public string EDRPOU { set; get; }
        public int? Year { set; get; }
        public int? Period { set; get; }
    }
}
