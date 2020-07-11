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
    public class SearchModel : PageModel
    {


		private readonly IConfiguration _config;

		private readonly ILogger<IndexModel> _logger;

		public SearchModel(IConfiguration config, ILogger<IndexModel> logger)
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
		/// This method binds to the search query to be used.
		/// </summary>
		[BindProperty(SupportsGet = true)]
		public string Query { get; set; }

		/// <summary>
		/// Searches for posts that either contain the tag or the search query (the search box).
		/// </summary>
		/// <param name="query">What to actually search for.</param>
		public void OnGet()
		{
			// If null there is nothing to search for, redirect to home.
			if (Query == null)
				RedirectToPage("/");

			using var context = new BlogContext(_config);
			// Load posts where either the content or the title contains the search query.
			Posts = context.Posts.Where(post => post.Content.Contains(Query) || post.Title.Contains(Query)).Include(post => post.Tags).ToList();

			// Always load the tags.
			Tags = context.Tags.ToList();
		}

	}
}
