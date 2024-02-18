using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Seed {
	public class BlogSeedData : IEntityTypeConfiguration<Blog> {
		public void Configure(EntityTypeBuilder<Blog> builder)
		{
			builder.HasData(new Blog
			{
				BlogTitle = "First blog",
				BlogShortDescription = "This a first blog",
				BlogDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus sapien sem, efficitur ut vulputate eu, efficitur eu turpis. Phasellus vel imperdiet leo. Suspendisse eu iaculis justo. Vestibulum consequat tempus ex at pulvinar. Pellentesque semper arcu nibh, id laoreet libero hendrerit vitae. Cras non luctus velit, ac rutrum odio. In mattis lobortis risus, ac vulputate urna tincidunt malesuada. Sed consequat sem sed lacus malesuada facilisis. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur arcu ligula, lobortis sed quam ac, dignissim interdum enim.",
				Image = "",
				BlogId = 1,
				UserId = 1,
				Comments = new List<Comment> { },
				Tags = new List<Tag> { },
			});
		}
	}
}
