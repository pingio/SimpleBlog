using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBlog.Models
{
	public class BlogContext : IdentityDbContext<SimpleUser>
	{
		private readonly IConfiguration _config;
		public BlogContext(IConfiguration config)
		{
			this._config = config;
		}

		public DbSet<Post> Posts { get; set; }
		public DbSet<Tag> Tags { get; set; }


		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			var conn = _config.GetConnectionString("SqliteConnection");
			options.UseSqlite(conn);
		}


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			// Creates a primary key: (PostID, TagName) for the Tag table.
			modelBuilder.Entity<Tag>()
				.HasKey(t => new { t.PostId, t.TagName });
		}
	}
}
