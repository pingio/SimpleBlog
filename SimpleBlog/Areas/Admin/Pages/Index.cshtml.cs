using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using SimpleBlog.Models;

namespace SimpleBlog.Areas.Admin.Pages
{
    public class IndexModel : PageModel
    {


        [BindProperty]
        public Post Post { get; set; }

        /// <summary>
        /// Because the tags are a simple string, we cannot use the Post.Tags field, this is used instead.
        /// </summary>
        [BindProperty]
        public string TagString { get; set; } = "";


        public List<Post> PreviousPosts { get; set; } = new List<Post>();

        /// <summary>
        /// OnGet, Load all the blog posts and their associated tags.
        /// </summary>
        public void OnGet()
        {
            using var context = new BlogContext();
            PreviousPosts = context.Posts.Include(posts => posts.Tags).ToList();
        }

        /// <summary>
        /// Checks if a new post is valid, if it is split the <see cref="TagString"/> into <seealso cref="Tag"/> for the post before committing changes.
        /// </summary>
        /// <returns>Returns the page to be loaded.</returns>
        public IActionResult OnPost()
        {
            using var context = new BlogContext();
            if (ModelState.IsValid)
            {
                
                var post = Post;
                post.Posted = DateTime.Now;

                var tags = new List<Tag>();
                foreach (var tagString in TagString.Split(','))
                {
                    var tag = new Tag { TagName = tagString, PostId = post.PostId };
                    tags.Add(tag);
                }

                post.Tags = tags;


                context.Add(post);
                context.SaveChanges();
            }
            PreviousPosts = context.Posts.Include(posts => posts.Tags).ToList();
            return Page();

        }
    }
}
