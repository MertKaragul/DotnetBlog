using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities {
	public class Blog {
        public int BlogId { get; set; }
        public string? BlogTitle { get; set; }
        public string? BlogDescription { get; set; }
        public string? BlogShortDescription { get; set; }
        public string? Image { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public IList<Comment>? Comments { get; set; }
		public IList<Tag>? Tags { get; set; }
	}
}
