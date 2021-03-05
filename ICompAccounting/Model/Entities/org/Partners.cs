using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ICompAccounting.Model.Entities.org
{
    [Table("Partners", Schema = "org")]
    public class Partner
    {
        [Key]
        public int? KOD { set; get; }
        public string KOD_ZKPO { set; get; }
        public string NAZVA_ORG { set; get; }
        public string PodNom { set; get; }
        public string NomSvid { set; get; }
        public string Adresa { set; get; }
        public string N_TEL { set; get; }
        public string Primitka { set; get; }
    }
}
