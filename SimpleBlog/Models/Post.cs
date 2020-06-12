using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

		public DateTime Posted { get; set; } = DateTime.Now;

		public string PostedBy { get; set; }

		[Display(Name = "Tags", Description = "Separated by comma.")]
		public List<Tag> Tags { get; set; }


	}
}
