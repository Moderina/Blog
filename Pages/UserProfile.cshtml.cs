using Blog.Data;
using Blog.Interfaces;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Pages
{
    public class UserProfileModel : PageModel
    {
        public PaginatedList<Post> Posts { get; set; }
        public List<Comment> Comments { get; set; }


        [BindProperty(SupportsGet = true)]
        public string UserId { get; set; }

        public IPostService _postService { get; set; }
        public IUserService _userService { get; set; }

        public UserProfileModel(IPostService postService, IUserService userService)
        {
            _postService = postService;
            _userService = userService;
        }
        public void OnGet(int pageIndex = 1)
        {
            Posts = _postService.GetUserPaginatedPosts(UserId);
            Comments = _postcontext.Comments.ToList();

        }
    }
}
