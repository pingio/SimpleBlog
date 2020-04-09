using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBlog.Models
{
	public class Post
	{
		[Key]
		public int PostId { get; set; }

		[Required]
		[Display(Name = "Title")]
		public string Title { get; set; }

		[Required]
		[Display(Name = "Post")]
		public string Content { get; set; }

		[Display(Name = "Cover Image")]
		public string CoverImage { get; set; }

		[Timestamp]
		public DateTime Posted { get; set; }

		public ICollection<Tag> Tags { get; set; }
	}
}
