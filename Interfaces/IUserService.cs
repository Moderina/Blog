using Blog.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Blog.Interfaces
{
    public interface IUserService
    {
        List<UserVM> GetUsers(string SearchTerm);
        public string GetUserName(string id);
        IdentityUser GetDeleteUser(string username);

        void DeleteUser(IdentityUser Username);
    }
}
