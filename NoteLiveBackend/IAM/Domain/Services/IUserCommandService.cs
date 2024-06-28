using NoteLiveBackend.IAM.Domain.Model.Aggregates;
using NoteLiveBackend.IAM.Domain.Model.Commands;
using NoteLiveBackend.IAM.Interfaces.Resources;

namespace NoteLiveBackend.IAM.Domain.Services;

public interface IUserCommandService
{
    Task<(User user, string token)> Handle(SignInCommand command);
    Task Handle(SignUpCommand command);
    //Task<(User user, string token)> AuthenticateAsync(string username, string password);
    Task UpdateUser(User command);

}