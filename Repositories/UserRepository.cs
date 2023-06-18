using Blog.Data;
using Blog.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace Blog.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<IdentityUser> _manager;

        public UserRepository(UserManager<IdentityUser> manager)
        {
            _manager = manager;
        }

        public IQueryable<IdentityUser> GetMatchingUsers(string SearchTerm)
        {
            if (SearchTerm == "") { return _manager.Users.AsQueryable(); }
            var matching = _manager.Users.Where(d => d.UserName == SearchTerm);
            return matching;
        }

        public string GetUserName(string id)
        {
            return _manager.Users.FirstOrDefault(_ => _.Id == id).UserName;
        }

        public async Task DeleteUser(IdentityUser user)
        {

            //var user = await _manager.FindByNameAsync(username);
            //IdentityUser user = await _manager.FindByNameAsync(username);
            //IdentityUser user = GetMatchingUsers(username).FirstOrDefault();
            System.Diagnostics.Debug.WriteLine("TTTTTTTTTTTTTTTT" + user.UserName + user.Id);
            System.Diagnostics.Debug.WriteLine(_manager);

            var result = await _manager.DeleteAsync(user);  
        }
    }
}
