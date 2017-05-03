using AutofacDapperDemo.Repository;
using AutofacDapperDemo.Core.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacDapperDemo.Service.Users
{
    public  interface IUserService:IDependency
    {
        Task<IEnumerable<User>> UserList();

        Task<String> Insert(User user);

        Task<int> Update(User user);

        Task<int> Delete(User user);

        Task<User> GetById(string id);
    }
}
