
using FluentValidation;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Exceptions;

namespace MyRecipeBook.Application.UseCases.User.Register
{
    public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
    {
        public RegisterUserValidator()
        {
            RuleFor(user => user.Name)
                .NotEmpty().WithMessage(ResourceMessageException.NAME_EMPTY)
                .MaximumLength(100).WithMessage(ResourceMessageException.NAME_EXCEED);
            RuleFor(user => user.Email)
                .NotEmpty().WithMessage(ResourceMessageException.EMAIL_EMPTY);
            RuleFor(user => user.Password)
                .NotEmpty().WithMessage(ResourceMessageException.PASSWORD_EMPTY)
                .MinimumLength(6).WithMessage(ResourceMessageException.PASSWORD_INVALID);
            When(user => string.IsNullOrEmpty(user.Email) == false, () =>
            {
                RuleFor(user => user.Email)
                .EmailAddress().WithMessage(ResourceMessageException.EMAIL_INVALID);
            });
        }
    }
}
