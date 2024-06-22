using System.Runtime.InteropServices;
using NoteLiveBackend.Shared.Domain.Repositories;


public class UserCommandService(IUserRepository userRepository,
    ITokenService tokenService,
    IHashingService hashingService,
    IUnitOfWork unitOfWork) : IUserCommandService
{
    public async Task<(User user, string token)> Handle(SignInCommand command)
    {
        var user = await userRepository.FindByUsernameAsync(command.username);
        if (user == null || !hashingService.VerifyPassword(command.password, user.PasswordHash))
            throw new Exception("Invalid username or password");
        var token = tokenService.GenerateToken(user);
        return (user, token);
    }

    public async Task Handle(SignUpCommand command)
    {
        if (userRepository.ExistsByUsername(command.Username))
            throw new Exception($"Username {command.Username} is already taken");

        var hashedPassword = hashingService.HashPassword(command.Password);
        var user = new User(command.Username, hashedPassword);
        try
        {
            await userRepository.AddSync(user);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception($"An error ocurred while creating user: {e.Message}");
        }
    }
}