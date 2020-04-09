using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleBlog.Models;

namespace SimpleBlog.Pages
{
    public class PostModel : PageModel
    {
        public Post Post { get; set; }
        public void OnGet(int id)
        {
            using var context = new BlogContext();
            Post = context.Posts.FirstOrDefault(post => post.PostId == id);

        }
    }
}
