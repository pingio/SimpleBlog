using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SimpleBlog.Models;

namespace SimpleBlog.Areas.Admin.Pages
{
    public class PostsModel : PageModel
    {
        private readonly IConfiguration _config;

        public PostsModel(IConfiguration config)
		{
            _config = config;
		}


        public void OnGet(string tagstring)
        {
            using var context = new BlogContext(_config);


            // Gets all posts where there is a tag with a TagName that matches the tagstring.
            Posts = context.Posts.
                Include(post => post.Tags).
                Where(post => post.Tags.Any(tag => tag.TagName == tagstring)).ToList();


        }

        public List<Post> Posts { get; private set; } = new List<Post>();
    }
}
