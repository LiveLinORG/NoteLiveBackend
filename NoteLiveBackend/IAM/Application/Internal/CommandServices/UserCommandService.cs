using NoteLiveBackend.IAM.Application.Internal.OutboundServices;
using NoteLiveBackend.IAM.Domain.Model.Aggregates;
using NoteLiveBackend.IAM.Domain.Model.Commands;
using NoteLiveBackend.IAM.Domain.Repositories;
using NoteLiveBackend.IAM.Domain.Services;
using NoteLiveBackend.Shared.Domain.Repositories;
namespace NoteLiveBackend.IAM.Application.Internal.CommandServices;

public class UserCommandService : IUserCommandService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IHashingService _hashingService; 

        public UserCommandService(IUserRepository userRepository, IUnitOfWork unitOfWork, ITokenService tokenService, IHashingService hashingService)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _hashingService = hashingService; 
        }

        public async Task<(User user, string token)> Handle(SignInCommand command)
        {
            var user = await _userRepository.FindByUsernameAsync(command.username);
            if (user == null || !_hashingService.VerifyPassword(command.password, user.PasswordHash))
                throw new Exception("Invalid credentials");

            var token = _tokenService.GenerateToken(user);
            return (user, token);
        }

        public async Task Handle(SignUpCommand command)
        {
            command.Validate(); // Validar los datos del comando SignUpCommand
            if (_userRepository.ExistsByUsername(command.Username))
                throw new Exception($"Username {command.Username} is already taken");

            var hashedPassword = _hashingService.HashPassword(command.Password); 
            var newUser = new User(command.Username, hashedPassword, command.Name, command.LastName, command.Correo, command.Role);
            
            try
            {
                await _userRepository.AddSync(newUser); 
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception($"An error occurred while creating user: {e.Message}");
            }
        }
    }