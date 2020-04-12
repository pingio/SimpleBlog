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

        public IndexModel()
        {
            
        }

        [BindProperty]
        public Post Post { get; set; }

        [BindProperty]
        public string TagString { get; set; } = "";


        public List<Post> PreviousPosts { get; set; } = new List<Post>();

        public void OnGet()
        {
            using var context = new BlogContext();

            PreviousPosts = context.Posts.Include(posts => posts.Tags).ToList();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                using var context = new BlogContext();
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
                PreviousPosts = context.Posts.Include(posts => posts.Tags).ToList();
            }
            return Page();

        }
    }
}
