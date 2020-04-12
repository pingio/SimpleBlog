using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBlog.Models
{
	public class BlogContext : DbContext
	{

		public DbSet<Post> Posts { get; set; }
		public DbSet<Tag> Tags { get; set; }


		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			options.UseInMemoryDatabase(databaseName: "SimpleBlog");
		}


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Tag>()
				.HasKey(t => new { t.PostId, t.TagName });
		}
	}
}
