using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.BlogDto
{
    public class CreateBlogDto
    {
        public string? BlogTitle { get; set; }
        public string? BlogShortDescription { get; set; }
        public string? BlogDescription { get; set; }
        public string? BlogImage { get; set; }
    }
}
