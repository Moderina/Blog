using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<IdentityUser> GetMatchingUsers(string SearchTerm);
        public string GetUserName(string id);
        Task DeleteUser(IdentityUser user);

    }
}
