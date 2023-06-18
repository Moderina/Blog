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

        private readonly PostContext _postcontext;

        private readonly IPostService _postservice;
        private readonly IUserService _userservice;

        [BindProperty]
        public IFormFile? Image { get; set; }

        [BindProperty]
        public Post Post { get; set; }

        [BindProperty]
        public Comment Comment { get; set; }

        //public List<Post> Posts { get; set; }
        public PaginatedList<Post> Posts { get; set; }

        public List<Comment> Comments { get; set; }

        public List<Tag> Tags { get; set; }


        public IndexModel(ILogger<IndexModel> logger, PostContext postContext, IPostService service, IUserService userService)
        {
            _logger = logger;
            _postcontext = postContext;
            _postservice = service;
            _userservice = userService;
        }

        public void OnGet(int pageIndex = 1)
        {
            Posts = _postservice.GetPosts(pageIndex);
            Tags = _postcontext.Tags.ToList();
            Comments = _postcontext.Comments.ToList();

            //foreach (var post in Posts) 
            //{   
            //    try
            //    {
            //        string[] temp = post.SerializedImages.Split("&");
            //        //System.Diagnostics.Debug.WriteLine("EEEEEEEEEE" + temp[0].Substring(0, 100));
            //        post.deserImages = temp.ToList();
            //        System.Diagnostics.Debug.WriteLine("IIIIIIIIIIIIIII" + post.SerializedImages.Substring(post.SerializedImages.Length - 10));
            //    }
            //    catch 
            //    {
            //        post.deserImages = new List<string>();
            //    }
            //}
        }

        public IActionResult OnPost(int pageIndex = 1)
        {

            Post.Author = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _postservice.AddPost(Post, Image);
            Posts = _postservice.GetPosts(pageIndex);
            return RedirectToPage();
        }

        public IActionResult OnPostComment(int Postid, int pageIndex = 1)
        {
            Comment.UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _postservice.AddComment(Comment, Postid);
            Posts = _postservice.GetPosts(pageIndex);
            return RedirectToPage();
        }

        public IActionResult OnPostDeleteComment(int comid, int pageIndex = 1)
        {
            System.Diagnostics.Debug.WriteLine("UUUUUUUUUUU" + comid);
            _postservice.DeleteComment(comid);
            Posts = _postservice.GetPosts(pageIndex);
            return RedirectToPage();
        }
    }
}