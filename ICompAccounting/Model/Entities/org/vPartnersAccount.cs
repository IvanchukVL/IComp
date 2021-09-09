using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ICompAccounting.Model.Entities.org
{
    public class vPartnersAccount
    {
        [Key]
        public int KOD { set; get; }
        public string KOD_ZKPO { set; get; }
        public string NAZVA_ORG { set; get; }
        public int AccountId { set; get; }
        public string IBAN { set; get; }
        public string Description { set; get; }
    }
}
