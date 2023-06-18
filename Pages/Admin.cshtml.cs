using Blog.Interfaces;
using Blog.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Pages
{
    public class AdminModel : PageModel
    {
        public UserVM UserVM { get; set; }
        public string StatusMessage { get; set; }

        [BindProperty]
        public List<UserVM> UsersVM { get; set; }

        private IUserService _userService;

        private readonly UserManager<IdentityUser> _manager;

        public AdminModel(IUserService userService, UserManager<IdentityUser> manager)
        {
            _userService = userService;
            _manager = manager;
        }
        public void OnGet()
        {
            UsersVM = _userService.GetUsers("");
        }

        public IActionResult OnPost(string delete_username)
        {
            System.Diagnostics.Debug.WriteLine("PPPPPPPPPPPP"+delete_username+"PPPPPPPPPPPPPPPPp");
            IdentityUser user = _userService.GetDeleteUser(delete_username);
            _userService.DeleteUser(user);
            StatusMessage = "User successfully destroyed";
            //UsersVM = _userService.GetUsers("");
            return Page();
        }
    }
}
