using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutofacDapperDemo.Models.Users
{
    public class UserModel : BaseModel
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public string Tel { get; set; }

        public string Adress { get; set; }

        public DateTime CreateDate { get; set; }
    }
}