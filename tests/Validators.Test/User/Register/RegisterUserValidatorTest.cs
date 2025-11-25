using CommonTestUtilities.Requests;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Exceptions;
using Shouldly;

namespace Validators.Test.User.Register
{
    public class RegisterUserValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validador = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();

            var result = validador.Validate(request);

            result.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Error_Name_Empty()
        {
            var validador = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();

            request.Name = string.Empty;

            var result = validador.Validate(request);

            result.IsValid.ShouldBeFalse();

            result.Errors.Count.ShouldBe(1);
            result.Errors[0].ErrorMessage.ShouldBe(ResourceMessageException.NAME_EMPTY);
        }

        [Fact]
        public void Error_Email_Empty()
        {
            var validador = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();

            request.Email = string.Empty;

            var result = validador.Validate(request);

            result.IsValid.ShouldBeFalse();

            result.Errors.Count.ShouldBe(1);
            result.Errors[0].ErrorMessage.ShouldBe(ResourceMessageException.EMAIL_EMPTY);
        }

        [Fact]
        public void Error_Email_Invalid()
        {
            var validador = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();

            request.Email = "email.com";

            var result = validador.Validate(request);

            result.IsValid.ShouldBeFalse();

            result.Errors.Count.ShouldBe(1);
            result.Errors[0].ErrorMessage.ShouldBe(ResourceMessageException.EMAIL_INVALID);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void Error_Password_Invalid(int passwordLength)
        {
            var validador = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build(passwordLength);

            var result = validador.Validate(request); 

            result.IsValid.ShouldBeFalse();

            result.Errors.Count.ShouldBe(1);
            result.Errors[0].ErrorMessage.ShouldBe(ResourceMessageException.PASSWORD_INVALID);
        }
    }
}
