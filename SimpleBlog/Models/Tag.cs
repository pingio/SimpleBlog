using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBlog.Models
{
	public class Tag
	{	
		
		public int PostId { get; set; }
		
		public string TagName { get; set; }

		[ForeignKey("PostId")]
		public Post Post { get; set; }


	}
}
