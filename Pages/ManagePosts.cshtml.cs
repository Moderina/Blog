using Blog.Interfaces;
using Blog.Models;
using Blog.Services;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Blog.Pages
{
    public class ManagePostsModel : PageModel
    {
        [BindProperty]
        public IFormFile Image { get; set; }

        [BindProperty]
        public Post Post { get; set; }

        [BindProperty]
        public List<Post> Posts { get; set; }

        private IPostService _postService;
        private IUserService _userService;

        public ManagePostsModel(IPostService postService, IUserService userService)
        {
            _postService = postService;
            _userService = userService;
        }

        public void OnGet()
        {
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Posts = _postService.getUsersPosts(userid);
        }

        public void OnPostDelete(int delete_post) 
        {
            _postService.DeletePost(delete_post);
            Posts = _postService.getUsersPosts(User.FindFirstValue(ClaimTypes.NameIdentifier));

        }

        public void OnPostEdit(string text, int edit_post)
        {
            System.Diagnostics.Debug.WriteLine(edit_post);
            System.Diagnostics.Debug.WriteLine(Request.Form["text"]);
            Post.Description = Request.Form["text"];
            _postService.EditPost(edit_post, Post, Image);
            Posts = _postService.getUsersPosts(User.FindFirstValue(ClaimTypes.NameIdentifier));

        }
    }
}
