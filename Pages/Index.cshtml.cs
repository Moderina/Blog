using Blog.Data;
using Blog.Interfaces;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Blog.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly IPostService _postService;
        private readonly IUserService _userservice;

        [BindProperty]
        public IFormFile? Image { get; set; }

        [BindProperty]
        public Post Post { get; set; }

        [BindProperty]
        public Comment Comment { get; set; }

        public PaginatedList<Post> Posts { get; set; }

        public List<Comment> Comments { get; set; }


        public IndexModel(ILogger<IndexModel> logger, IPostService service, IUserService userService)
        {
            _logger = logger;
            _postService = service;
            _userservice = userService;
        }

        public void OnGet(int pageIndex = 1)
        {
            Posts = _postService.GetPosts(pageIndex);
            Comments = _postService.GetComments();
        }

        public IActionResult OnPost(int pageIndex = 1)
        {

            Post.Author = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _postService.AddPost(Post, Image);
            Posts = _postService.GetPosts(pageIndex);
            return RedirectToPage();
        }

        public IActionResult OnPostComment(int Postid, int pageIndex = 1)
        {
            Comment.UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Comment.AddressIP = HttpContext.Connection.RemoteIpAddress?.ToString();
            _postService.AddComment(Comment, Postid);
            Posts = _postService.GetPosts(pageIndex);
            return RedirectToPage();
        }

        public IActionResult OnPostDeleteComment(int comid, int pageIndex = 1)
        {
            _postService.DeleteComment(comid);
            Posts = _postService.GetPosts(pageIndex);
            return RedirectToPage();
        }
    }
}