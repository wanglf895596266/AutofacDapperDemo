using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacDapperDemo.Core.Log
{
    public class Logs : BaseEntity
    {
        public string Message { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
