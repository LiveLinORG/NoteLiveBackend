using NoteLiveBackend.IAM.Domain.Model.Aggregates;
using NoteLiveBackend.IAM.Domain.Model.Queries;
using NoteLiveBackend.IAM.Domain.Repositories;
using NoteLiveBackend.IAM.Domain.Services;

namespace NoteLiveBackend.IAM.Application.Internal.QueryServices;

public class UserQueryService : IUserQueryServices
{
    private readonly IUserRepository _userRepository;

    public UserQueryService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> Handle(GetUserByIdQuery query)
    {
        return await _userRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<User>> Handle(GetAllUsersQuery query)
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<IEnumerable<User>> Handle(GetUserByNameQuery query)
    {
        return await _userRepository.FindByNameAsync(query.Name);
    }

    public async Task<User?> Handle(GetUserByNameAndCorreoQuery query)
    {
        return await _userRepository.FindByNameAndCorreoAsync(query.Name, query.Correo);
    }

    public async Task<User?> Handle(GetUserByCodigoProfesorQuery query)
    {
        return await _userRepository.FindByCodigoProfesorAsync(query.CodigoProfesor);
    }


}