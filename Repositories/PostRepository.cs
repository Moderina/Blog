using Blog.Data;
using Blog.Interfaces;
using Blog.Models;
using Microsoft.AspNetCore.Identity;
using System.Drawing;

namespace Blog.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly PostContext _context;

        public PostRepository(PostContext context) { _context = context; }
        
//////////////////////POSTS

        public IQueryable<Post> getUserPosts(string userid)
        {
            return _context.Posts.Where(_ => _.Author == userid).OrderByDescending(x => x.Id);
        }

        public List<Post> GetPosts()
        {
            return _context.Posts.OrderByDescending(x => x.Id).ToList();
        }

        public Post getPost(int id)
        {
            return _context.Posts.FirstOrDefault(_ => _.Id == id);
        }

        public void AddPost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        public void EditPost(int id, Post newpost)
        {
            Post post = getPost(id);
            if (post.Title != newpost.Title) post.Title = newpost.Title;
            if (post.Description != newpost.Description) post.Description = newpost.Description;
            if (post.SerializedImages != newpost.SerializedImages && newpost.SerializedImages != null) post.SerializedImages = newpost.SerializedImages;
            post.Tags = newpost.Tags;
            _context.SaveChanges();
        }

        public void DeletePost(int id)
        {
            Post post = getPost(id);
            _context.Remove(post);
            _context.SaveChanges();
        }

//////////////////////COMMENTS

        public Comment GetComment(int id)
        {
            return _context.Comments.Where(_ => _.Id == id).FirstOrDefault();
        }

        public void AddComment(Comment comment, int postid)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
            int id = _context.Comments.OrderBy(_ => _.Id).Last().Id;
            Post post = getPost(postid);
            post.Commends += id.ToString() + "&";
            _context.SaveChanges();

        }

        public void DeleteComment(Comment comment, int postid)
        {
            int comid = comment.Id;
            _context.Comments.Remove(comment);
            Post post = getPost(postid);
            string comments = post.Commends;
            int index = comments.IndexOf("&" + comid + "&");
            post.Commends = comments.Remove(index + 1, 2);
            _context.SaveChanges();
        }

        public void DeleteComment(int id)
        {
            Comment comment = _context.Comments.FirstOrDefault(c => c.Id == id);
            _context.Comments.Remove(comment);
            _context.SaveChanges();
        }
 





//////////////////////TAGS

        public List<Tag> GetTags(List<string> tags)
        {
            List<Tag> result = new List<Tag>();
            foreach (string tag in tags)
            {
                result.Add(_context.Tags.First(_ => _.Name == tag));
            }
            return result;
        }

        public IQueryable<Tag> tagExists(string tag)
        {
            return _context.Tags.Where(d => d.Name == tag).AsQueryable();
        }

        public void changeTagCount(string name, int val)
        {
            Tag tag = _context.Tags.FirstOrDefault(_ => _.Name == name);
            if (tag.Count + val == 0)
            {
                _context.Tags.Remove(tag);
                return;
            }
            tag.Count += val;
            _context.SaveChanges();
            return;
        }

        public void addNewTag(string name)
        {
            Tag newTag = new Tag();
            newTag.Name = name;
            newTag.Count = 1;
            _context.Tags.Add(newTag);
            _context.SaveChanges();
            return;
        }

    }
}
