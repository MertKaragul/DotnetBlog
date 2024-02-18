using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos {
    public class TagDto {
        public int TagId { get; set; }
        public string? Name { get; set; }
        public IEnumerable<BlogDto>? Blogs { get; set; }
    }
}
