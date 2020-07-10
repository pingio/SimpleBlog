using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SimpleBlog.Models;

namespace SimpleBlog.Pages
{
    public class PostModel : PageModel
    {

        private readonly IConfiguration _config;

        public PostModel(IConfiguration config)
		{
            _config = config;
		}
        public Post Post { get; set; }

		public List<Tag> Tags { get; set; }

		public void OnGet(int id)
        {
            using var context = new BlogContext(_config);
            Post = context.Posts.Include(post => post.Tags).FirstOrDefault(post => post.PostId == id);
			Tags = context.Tags.ToList();

		}
    }
}
