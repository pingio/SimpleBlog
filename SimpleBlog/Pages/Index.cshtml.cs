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
	public class IndexModel : PageModel
	{
		private readonly IConfiguration _config;

		private readonly ILogger<IndexModel> _logger;


		public IndexModel(IConfiguration config, ILogger<IndexModel> logger)
		{
			_config = config;
			_logger = logger;
			
		}

		public List<Post> PreviousPosts { get; set; }

		public void OnGet()
		{
			using var context = new BlogContext(_config);
			PreviousPosts = context.Posts.Include(post => post.Tags).ToList();

		}
	}
}
