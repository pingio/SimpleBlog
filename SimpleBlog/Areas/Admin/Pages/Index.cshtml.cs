using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using SimpleBlog.Models;

namespace SimpleBlog.Areas.Admin.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _config;
        private readonly SignInManager<SimpleUser> _signin;
        private readonly UserManager<SimpleUser> _user;

        public IndexModel(IConfiguration config, SignInManager<SimpleUser> signin, UserManager<SimpleUser> user) {
            _config = config;
            _signin = signin;
            _user = user;
        }

        [BindProperty]
        public Post Post { get; set; }

        /// <summary>
        /// Because the tags are a simple string, we cannot use the Post.Tags field, this is used instead.
        /// </summary>
        [BindProperty]
        public string TagString { get; set; } = "";


        public List<Post> PreviousPosts { get; set; } = new List<Post>();

        /// <summary>
        /// OnGet, Load all the blog posts and their associated tags.
        /// </summary>
        public IActionResult OnGet()
        {
			if (!_signin.IsSignedIn(User))
			{
                return RedirectToPage("/Account/Login", new { area = "Identity" });
			}

            using var context = new BlogContext(_config);
            PreviousPosts = context.Posts.Include(posts => posts.Tags).ToList();

            return Page();
        }

        /// <summary>
        /// Checks if a new post is valid, if it is split the <see cref="TagString"/> into <seealso cref="Tag"/> for the post before committing changes.
        /// </summary>
        /// <returns>Returns the page to be loaded.</returns>
        public async Task<IActionResult> OnPostAsync()
        {
         
            if (!_signin.IsSignedIn(User))
                return RedirectToPage("/Account/Login", new { area = "Identity" });

            var user = await _user.GetUserAsync(User);

            using var context = new BlogContext(_config);
            if (ModelState.IsValid)
            {

                var post = Post;
                var tags = new List<Tag>();
                

                if (TagString != null)
                {

                    foreach (var tagString in TagString.Split(','))
                        tags.Add(new Tag { TagName = tagString.Trim().Replace(" ", "-"), PostId = post.PostId });
                }

                post.Tags = tags;

                post.Posted = DateTime.Now;
                post.PostedBy = await _user.GetUserNameAsync(user);
                context.Posts.Add(post);
                context.SaveChanges();
            }

            PreviousPosts = context.Posts.Include(posts => posts.Tags).ToList();
            return Page();
        }
    
        public async Task<IActionResult> OnPostDeleteAsync()
        {
            using var context = new BlogContext(_config);
            context.Tags.RemoveRange(context.Tags);
            context.Posts.RemoveRange(context.Posts);
            await context.SaveChangesAsync();


            return RedirectToPage("/Index");


        }
    }

}
