using BlogManagement.Domain.Entities;
using BlogManagement.Persistance.Contract;
using BlogManagement.Persistance.DataBase;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BlogManagement.Persistance
{
    public class UserRepository : IUserRepository
    {
        private readonly IDataBase _dataBase;

        public UserRepository(IDataBase dataBase)
        {
            _dataBase = dataBase;
        }

        public async Task<User> GetUserByIdAsync(long userId)
        {
            try
            {
                return await Task.FromResult(_dataBase.Users.FirstOrDefault(u => u.UserId == userId));
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
