using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SimpleBlog.Models;

namespace SimpleBlog.Pages.Search
{
    public class TagModel : PageModel
    {


		private readonly IConfiguration _config;

		private readonly ILogger<IndexModel> _logger;

		public TagModel(IConfiguration config, ILogger<IndexModel> logger)
		{
			_config = config;
			_logger = logger;

		}
		/// <summary>
		/// These are the posts that are being loaded.
		/// </summary>
		public List<Post> Posts { get; set; }

		/// <summary>
		/// Tags for the sidebar.
		/// </summary>
		public List<Tag> Tags { get; set; }

		/// <summary>
		/// Search query.
		/// </summary>
		[BindProperty(SupportsGet = true)]
		public string Query { get; set; }

		/// <summary>
		/// Searches for posts that contain the tag name.
		/// </summary>
		/// <param name="query">What to actually search for.</param>
		public void OnGet()
		{
			// If null there is nothing to search for, redirect to home.
			if (Query == null)
				RedirectToPage("/");

			using var context = new BlogContext(_config);

			// TODO: Change so that the linq query only selects posts where there is a tag with the TagName == query. 
			var posts = context.Posts.Include(post => post.Tags).ToList();

			// This is an awful temporary workaround for querying proper posts with linq. 
			// Removes all the posts where there is no tag with that name.
			foreach (var p in posts)
			{
				// This does not work wwith Posts, using local variable before assignment.
				if (!p.Tags.Any(tag => tag.TagName == Query))
					posts.Remove(p);


			}
			Posts = posts;

			// Always load the tags.
			Tags = context.Tags.ToList();
		}
	
	}
}
