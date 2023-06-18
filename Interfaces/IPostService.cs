using Blog.Models;

namespace Blog.Interfaces
{
    public interface IPostService
    {
        public List<Post> getUsersPosts(string name);
        public PaginatedList<Post> GetUserPaginatedPosts(string userid, int pageIndex = 1);
        public PaginatedList<Post> GetPosts(int pageIndex = 1);
        public void AddPost(Post post, IFormFile Image);
        public void EditPost(int id, Post post, IFormFile Image);
        public void DeletePost(int id);

        public void AddComment(Comment comment, int postid);
        public void DeleteComment(int comid);


        public List<Tag> GetTags(List<string> tags);
        public bool tagExists(string tag);
        public void changeTagCount(string name, int val);
        public void newTag(string name);


    }
}
