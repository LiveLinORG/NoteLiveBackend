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

    public UserCommandService(IUserRepository userRepository, IUnitOfWork unitOfWork, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
    }

    public async Task<User> Handle(CreateUserCommand command)
    {
        if (command.CodigoProfesor.HasValue)
        {
            var profesor = await _userRepository.FindByCodigoProfesorAsync(command.CodigoProfesor.Value);
            if (profesor != null)
                throw new Exception("Professor with this code already exists");

            var newProfesor = new User(command.Correo, command.Password, "Profesor", command.Name, command.LastName, command.Correo, command.CodigoProfesor.Value);
            await _userRepository.AddAsync(newProfesor);
        }
        else
        {
            var alumno = await _userRepository.FindByCorreoAlumnoAsync(command.Correo);
            if (alumno != null)
                throw new Exception("Student with this email already exists");

            var newAlumno = new User(command.Correo, command.Password, "Alumno", command.Name, command.LastName, command.Correo);
            await _userRepository.AddAsync(newAlumno);
        }

        await _unitOfWork.CompleteAsync();
        return await _userRepository.FindByUsernameAsync(command.Correo);
    }

    public async Task<(User user, string token)> Handle(SignInCommand command)
    {
        var user = await _userRepository.FindByUsernameAsync(command.username);
        if (user == null || user.Password != command.password) // Assuming plain text comparison for simplicity
            throw new Exception("Invalid credentials");

        var token = _tokenService.GenerateToken(user);
        return (user, token);
    }

    public async Task Handle(SignUpCommand command)
    {
        command.Validate(); // Validar los datos del comando SignUpCommand

        var existingUser = await _userRepository.FindByUsernameAsync(command.Username);
        if (existingUser != null)
            throw new Exception("User with this username already exists");

        // Utilizar CreateUserCommand para crear el usuario
        var createUserCommand = new CreateUserCommand(command.Name, command.LastName, command.Correo, command.Password, command.CodigoProfesor);
        await Handle(createUserCommand); // Llamar al método Handle(CreateUserCommand) para crear el usuario

        await _unitOfWork.CompleteAsync();
    }
}