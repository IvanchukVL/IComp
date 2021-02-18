using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ICompAccounting.Model
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

    public class vUser
    {
        [Key]
        public int Id { set; get; }
        public string Login { set; get; }
        public string PIB { set; get; }
    }

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
        //public void Copy(Enterprise c)
        //{
        //    Id = c.Id;
        //    Name = c.Name;
        //    Account = c.Account;
        //    MFO = c.MFO;
        //    EDRPOU = c.EDRPOU;
        //    Year = c.Year;
        //    Period = c.Period;
        //}
    }

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

    public class Period
    {
        [Key]
        public int Id { set; get; }
        public string Code { set; get; }
        public string Description { set; get; }
        public int? ParentId { set; get; }
        public int? Status { set; get; }
    }

    public class Result
    {
        public Result(bool res)
        {
            Res = res;
        }

        public Result(bool res, string code, string message)
        {
            Res = res;
            Code = code;
            Message = message;

        }
        public bool Res { set; get; }
        public string Code { set; get; }
        public string Message { set; get; }
    }

}
