using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutofacDapperDemo.Models.Log
{
    public class LogModel : BaseModel
    {
        public string Message { get; set; }

        public DateTime CreateDate { get; set; }
    }
}