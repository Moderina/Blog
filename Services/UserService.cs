using Blog.Interfaces;
using Blog.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Blog.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<UserVM> GetUsers(string SearchTerm)
        {
            var query = _userRepository.GetMatchingUsers(SearchTerm);

            List<UserVM> users = new List<UserVM>();

            foreach (var person in query)
            {
                var pVM = new UserVM()
                {
                    Username = person.UserName,
                    Email = person.Email,
                };
                users.Add(pVM);
            }
            return users;
        }

        public string GetUserName(string id)
        {
            return _userRepository.GetUserName(id);
        }

        public IdentityUser GetDeleteUser(string username)
        {
            return _userRepository.GetMatchingUsers(username).FirstOrDefault();
        }

        public void DeleteUser(IdentityUser Username )
        {
            /*            var query = _userRepository.GetMatchingUsers(Username);
                        System.Diagnostics.Debug.WriteLine("AAAAAAAAAAAAAAA" + query.First());
                        _userRepository.DeleteUser(query.FirstOrDefault());*/
            _userRepository.DeleteUser(Username);
        }
    }
}
