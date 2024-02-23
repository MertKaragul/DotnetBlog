using Core.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.BlogDto
{
    public class BlogDto
    {
        public string? BlogTitle { get; set; }
        public string? BlogDescription { get; set; }
        public string? BlogShortDescription { get; set; }
        public string? Image { get; set; }
        public int UserId { get; set; }
        public UserDto? User { get; set; }
        public IList<CommentDto>? Comments { get; set; }
        public IList<TagDto>? Tags { get; set; }
    }
}
