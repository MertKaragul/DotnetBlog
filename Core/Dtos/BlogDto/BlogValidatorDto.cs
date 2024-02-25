using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.BlogDto
{
    public class BlogDtoValidator : AbstractValidator<CreateBlogDto>
    {
        public BlogDtoValidator()
        {
            RuleFor(x => x.BlogTitle).NotEmpty().WithMessage("Blog title required");
            RuleFor(x => x.BlogShortDescription).NotEmpty().WithMessage("Blog short description required");
            RuleFor(x => x.BlogDescription).NotEmpty().WithMessage("Blog description required");
            RuleFor(x => x.Image).NotEmpty().WithMessage("Blog image required");
        }
    }
}
