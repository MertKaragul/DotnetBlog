using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Seed {
	public class UserSeedData : IEntityTypeConfiguration<User> {
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.HasData(new User
			{
				UserId = 1,
				UserName = "TEXAST5",
				Email = "texast5@gmail.com",
				Password = "123456",
				Roles = Core.Enums.Roles.ADMIN,
				Image = ""
			});
		}
	}
}
