using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SimpleBlog.Models;

namespace SimpleBlog.Pages
{
    public class SearchModel : PageModel
    {


		private readonly IConfiguration _config;

		private readonly ILogger<IndexModel> _logger;

		public SearchModel(IConfiguration config, ILogger<IndexModel> logger)
		{
			_config = config;
			_logger = logger;

		}

		public List<Post> Posts { get; set; }

		public List<Tag> Tags { get; set; }

		/// <summary>
		/// Searches for posts that either contain the tag or the search query (the search box).
		/// </summary>
		/// <param name="search">Can either be "tag" or "post", determines what to search for.</param>
		/// <param name="query">What to actually search for.</param>
		public void OnGet(string search, string query)
        {
			if (search.ToLower() != "tag" && search.ToLower() != "post")
				RedirectToPage();

			using var context = new BlogContext(_config);
			if (search.ToLower() == "tag")
			{

				// TODO: Change so that the linq query only selects posts where there is a tag with the TagName == query. 
				Posts = context.Posts.Include(post => post.Tags).ToList();

				// This is an awful temporary workaround for querying proper posts with linq. 
				// Removes all the posts where there is no tag with that name.
				foreach(var p in Posts)
				{
					if (!p.Tags.Any(tag => tag.TagName == query))
						Posts.Remove(p);
				}
			}
			else
			{
				// Load posts where either the content or the title contains the search query.
				Posts = context.Posts.Where(post => post.Content.Contains(query) || post.Title.Contains(query)).ToList();
				
			}
			// Always load the tags.
			Tags = context.Tags.ToList();
		}
    }
}
