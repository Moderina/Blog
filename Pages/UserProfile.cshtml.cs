using Blog.Data;
using Blog.Interfaces;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Blog.Pages
{
    public class UserProfileModel : PageModel
    {
        public PaginatedList<Post> Posts { get; set; }
        public List<Comment> Comments { get; set; }

        [BindProperty]
        public Comment Comment { get; set; }

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
            Comments = _postService.GetComments();
        }
        public IActionResult OnPostComment(int Postid, int pageIndex = 1)
        {

            Comment.UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Comment.AddressIP = HttpContext.Connection.RemoteIpAddress?.ToString();
            _postService.AddComment(Comment, Postid);
            Posts = _postService.GetPosts(pageIndex);
            Comments = _postService.GetComments();
            return Page();
        }

        public IActionResult OnPostDeleteComment(int comid, int pageIndex = 1)
        {
            _postService.DeleteComment(comid);
            Posts = _postService.GetPosts(pageIndex);
            Comments = _postService.GetComments();
            return Page();
        }
    }
}
