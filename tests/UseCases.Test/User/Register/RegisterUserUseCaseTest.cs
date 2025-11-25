using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;
using Shouldly;

namespace UseCases.Test.User.Register
{
    public class RegisterUserUseCaseTest
    {
        [Fact]
        public async Task Success()
        {

            var request = RequestRegisterUserJsonBuilder.Build();

            var useCase = CreateUseCase();

            var result = await useCase.Execute(request);

            result.ShouldNotBeNull();

            result.Name.ShouldBe(request.Name);
        }

        [Fact]
        public async Task Error_Email_already_registered()
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            var useCase = CreateUseCase(request.Email);
            
            Func<Task> act = async () => await useCase.Execute(request);

            var exception = await act.ShouldThrowAsync<ErrorOnValidationException>();
            exception.ErrorMessages.Count.ShouldBe(1);
            exception.ErrorMessages.ShouldContain(ResourceMessageException.EMAIL_ALREADY_REGISTERED);
        }

        [Fact]
        public async Task Error_Name_Empty()
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Name = string.Empty;
            var useCase = CreateUseCase();

            Func<Task> act = async () => await useCase.Execute(request);

            var exception = await act.ShouldThrowAsync<ErrorOnValidationException>();
            exception.ErrorMessages.Count.ShouldBe(1);
            exception.ErrorMessages.ShouldContain(ResourceMessageException.NAME_EMPTY);
        }

        private static RegisterUserUseCase CreateUseCase(string? email = null)
        {
            var mapper = MapperBuilder.Build();
            var passwordEncripter = PasswordEncripterBuilder.Build();
            var writeRepository = UserWriteOnlyRepositoryBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();
            var readRepositoryBuilder = new UserReadOnlyRepositoryBuilder();

            if(string.IsNullOrEmpty(email) == false)
            {
                readRepositoryBuilder.ExistActiveUserWithEmail(email);
            }

            return new RegisterUserUseCase(writeRepository, readRepositoryBuilder.Build(), mapper, passwordEncripter, unitOfWork);
        }
    }
}
