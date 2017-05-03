using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutofacDapperDemo.Core.User;
using AutofacDapperDemo.Repository;

namespace AutofacDapperDemo.Service.Users
{
    public class UserService : IUserService
    {
        private readonly IDemoRepository<User, String> _demoRepository;

        public UserService(IDemoRepository<User, String> demoRepository)
        {
            _demoRepository = demoRepository;
        }


        public async Task<String> Insert(User user)
        {
            return await _demoRepository.InsertAsync(user);
        }

        public async Task<int> Update(User user)
        {
            return await _demoRepository.UpdateAsync(user);
        }

        public async Task<int> Delete(User user)
        {
            return await _demoRepository.DeleteAsync(user.Id.ToString());
        }


        public async Task<IEnumerable<User>> UserList()
        {
            return await _demoRepository.GetListAsync();
        }


        public async Task<User> GetById(string id)
        {
            return await _demoRepository.GetAsync(id);
        }
    }
}
