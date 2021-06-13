using BlogManagement.Domain.Entities;
using System.Threading.Tasks;

namespace BlogManagement.Persistance.Contract
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(long userId);
    }
}
