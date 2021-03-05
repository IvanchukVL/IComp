using System;
using System.Collections.Generic;
using System.Text;

namespace ICompAccounting.Model.Entities
{
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
