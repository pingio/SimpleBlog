using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using SimpleBlog.Models;

namespace SimpleBlog.Areas.Admin.Pages
{
    public class IndexModel : PageModel
    {

        public IndexModel()
        {
            using(var context = new BlogContext())
            {
                PreviousPosts = context.Posts.ToList();
            }
        }

        [BindProperty]
        public Post Post { get; set; }
        public List<Post> PreviousPosts { get; set; }

        public void OnGet()
        {
            
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                using var context = new BlogContext();
                var post = Post;
                post.Posted = DateTime.Now;

                context.Add(post);
                context.SaveChanges();
                PreviousPosts = context.Posts.ToList();
            }
            return Page();

        }
    }
}
