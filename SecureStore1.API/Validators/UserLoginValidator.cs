using FluentValidation;
using SecureStore1.API.DTOs;

namespace SecureStore1.API.Validators
{
    public class UserLoginValidator : AbstractValidator<LoginUserDto>
    {
        public UserLoginValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required.")  // Ensure username is not empty
                .MinimumLength(4).WithMessage("Username must be at least 4 characters long.")
                .MaximumLength(20).WithMessage("Username must be no longer than 20 characters.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")  // Ensure password is not empty
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")  // Minimum length for password
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")  // Require at least one uppercase letter
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")  // Require at least one lowercase letter
                .Matches("[0-9]").WithMessage("Password must contain at least one number.")  // Require at least one number
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");  // Require at least one special character
        }
    }
}
