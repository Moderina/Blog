using Blog.Interfaces;
using Blog.Models;
using Blog.Pages;
using Microsoft.Extensions.Hosting;
using System.Text.RegularExpressions;

namespace Blog.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _repo;

        public PostService (IPostRepository repo)
        {
            _repo = repo;
        }
//////////////////////////POSTS

        public Post GetPost(int id)
        {
            return _repo.getPost(id);
        }

        public List<Post> getUsersPosts(string userid)
        {
            return _repo.getUserPosts(userid).ToList();
        }

        public PaginatedList<Post> GetUserPaginatedPosts(string userid, int pageIndex = 1)
        {
            var posts = _repo.getUserPosts(userid).ToList();
            var plist = PaginatedList<Post>.Create(posts, pageIndex, 20);
            return plist;
        }
        public PaginatedList<Post> GetPosts(int pageIndex = 1)
        {
            var posts = _repo.GetPosts();
            var plist = PaginatedList<Post>.Create(posts, pageIndex, 10);
            return plist;
        }


        public void AddPost(Post post, IFormFile Image)
        {
            post.SerializedImages = ConvertImage(Image);

            string pattern = @"(?<=#)\w+";
            MatchCollection tags = Regex.Matches(post.Description, pattern);

            foreach (Match match in tags)
            {
                if (tagExists(match.Value))
                {
                    changeTagCount(match.Value, 1);
                }
                else
                {
                    newTag(match.Value);
                }
            }
            List<string> tg = tags.OfType<Match>().Select(match => match.Value).ToList();
            post.Tags = string.Join(',', tg);

            post.date = DateTime.Now;
            post.Commends = "&";
            _repo.AddPost(post);
        }

        public void EditPost(int id, Post post, IFormFile Image)
        {
            post.SerializedImages = ConvertImage(Image);
            post.Tags = manageTags(post.Description, id);
            _repo.EditPost(id, post); 
        }

        public void DeletePost(int id)
        {
            Post post = _repo.getPost(id);
            string[] tags = post.Tags.Split(',');
            if (tags[0] != "")
            {
                foreach (string tag in tags)
                {
                    changeTagCount(tag, -1);
                }
            }
            string[] comments = post.Commends.Split('&');
            if (comments.Count() > 1)
            {
                foreach (string comment in comments)
                {
                    if (comment != "")
                    {
                        _repo.DeleteComment(int.Parse(comment));
                    }
                }
            }
            System.Diagnostics.Debug.WriteLine(comments);
            _repo.DeletePost(id);
        }

        private string ConvertImage(IFormFile Image)
        {
            byte[] bytes = null;

            if (Image != null)
            {
                using (Stream fs = Image.OpenReadStream())
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        bytes = br.ReadBytes((Int32)fs.Length);
                    }
                }
                return Convert.ToBase64String(bytes, 0, bytes.Length);
            }
            else
            {
                return "";
            }
        }



        public void AddComment(Comment comment, int postid)
        {
            comment.PostID = postid;
            comment.Date = DateTime.Now;
            _repo.AddComment(comment, postid);
        }

        public void DeleteComment(int comid)
        {
            Comment comment = _repo.GetComment(comid);
            System.Diagnostics.Debug.WriteLine("FFFFFFFFFFFFFF" + comment);
            _repo.DeleteComment(comment, comment.PostID);
        }


///////////////////////////////TAGS

        public List<Tag> GetTags(List<string> tags)
        {
            return _repo.GetTags(tags);
        }

        public bool tagExists(string tag)
        {
            var querry = _repo.tagExists(tag);
            if (querry.Any()) return true;
            else return false;
        }

        private bool tagExistsIn(int postid, string tag)
        {
            var post = _repo.getPost(postid);
            if (post.Tags.Contains(tag)) return true;
            else return false;
        }

        public void changeTagCount(string name, int val)
        {
            _repo.changeTagCount(name, val);
        }

        public void newTag(string name)
        {
            _repo.addNewTag(name);
        }

        private string manageTags(string text, int id)
        {
            string pattern = @"(?<=#)\w+";
            MatchCollection tags = Regex.Matches(text, pattern);

            foreach (Match match in tags)
            {
                if (tagExists(match.Value))
                {
                    if (!tagExistsIn(id, match.Value))
                    {
                        changeTagCount(match.Value, 1);
                    }
                }
                else
                {
                    newTag(match.Value);
                }
            }
            List<string> tg = tags.OfType<Match>().Select(match => match.Value).ToList();

            Post post = _repo.getPost(id);
            string[] ttags = post.Tags.Split(',');
            if (ttags[0] != "")
            {
                foreach (string tag in ttags)
                {
                    if (!tg.Contains(tag))
                    {
                        changeTagCount(tag, -1);
                    }
                }
            }
            return string.Join(",", tg);

        }
    }
}
