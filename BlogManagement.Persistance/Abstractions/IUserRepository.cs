using BlogManagement.Domain.Abstractions;
using System.Threading.Tasks;

namespace BlogManagement.Persistance.Abstractions
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(long userId);
    }
}
