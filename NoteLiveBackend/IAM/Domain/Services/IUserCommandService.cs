using NoteLiveBackend.IAM.Domain.Model.Aggregates;
using NoteLiveBackend.IAM.Domain.Model.Commands;

namespace NoteLiveBackend.IAM.Domain.Services;

public interface IUserCommandService
{
    Task<(User user, string token)> Handle(SignInCommand command);
    Task<User> Handle(CreateUserCommand command);
    Task Handle(SignUpCommand command);
}