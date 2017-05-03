using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacDapperDemo.Core.User
{
    public class User : BaseEntity
    {
        private DateTime createDate;

        public string Name { get; set; }

        public int Age { get; set; }

        public string Tel { get; set; }

        public string Adress { get; set; }

        public DateTime CreateDate
        {
            get
            {
                return createDate;
            }
            set
            {
                createDate = DateTime.Now;
            }
        }
    }
}
