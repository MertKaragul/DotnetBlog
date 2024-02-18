using Core.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities {
	public class User {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Image { get; set; }
        public Roles Roles { get; set; } = Roles.USER;
    }

    public class UserValidation : AbstractValidator<User>
    {
        public UserValidation() {
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password cannot be empty");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email cannot be empty").EmailAddress().WithMessage("Geçerli bir email adresi giriniz");
        }
    }
}
