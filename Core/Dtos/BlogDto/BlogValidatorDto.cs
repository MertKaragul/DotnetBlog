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
            RuleFor(x => x.BlogTitle).Empty().WithMessage("Blog title required");
            RuleFor(x => x.BlogShortDescription).Empty().WithMessage("Blog short description required");
            RuleFor(x => x.BlogDescription).Empty().WithMessage("Blog description required");
            RuleFor(x => x.BlogImage).Empty().WithMessage("Blog image required");
        }
    }
}
