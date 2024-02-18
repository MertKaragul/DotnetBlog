using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities {
	public class Tag {
        public int TagId { get; set; }
        public string? Name { get; set; }
        public IEnumerable<Blog>? Blogs { get; set; }

    }
}
