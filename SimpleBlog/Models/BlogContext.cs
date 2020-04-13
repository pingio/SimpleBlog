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
			options.UseInMemoryDatabase(databaseName: "SimpleBlog"); //This is only for testing purposes. Will change to SQLServer/MySQL.
		}


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Creates a primary key: (PostID, TagName) for the Tag table.
			modelBuilder.Entity<Tag>()
				.HasKey(t => new { t.PostId, t.TagName });
		}
	}
}
