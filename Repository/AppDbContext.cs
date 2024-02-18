using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repository {
	public class AppDbContext : DbContext {

		public AppDbContext(DbContextOptions<AppDbContext> contextOptions) : base(contextOptions) { }

		public DbSet<User> Users {get;set;}
		public DbSet<Comment> Comment {get;set;}
		public DbSet<Tag> Tags {get;set;}
		public DbSet<Blog> Blogs {get;set;}


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			base.OnModelCreating(modelBuilder);
		}
	}
}
