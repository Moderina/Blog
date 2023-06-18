using Blog.Models;

namespace Blog.Interfaces
{
    public interface IPostRepository
    {
        public IQueryable<Post> getUserPosts(string username);
        public List<Post> GetPosts();
        public Post getPost(int id);
        public void AddPost(Post post);
        public void EditPost(int id, Post newpost);

        public void DeletePost(int id);

        public Comment GetComment(int id);
        public void AddComment(Comment comment, int postid);
        public void DeleteComment(Comment comment, int postid);
        public void DeleteComment(int id);



        public List<Tag> GetTags(List<string> tags);

        public IQueryable<Tag> tagExists(string tag);
        public void changeTagCount(string name, int val);
        public void addNewTag(string name);

    }
}
