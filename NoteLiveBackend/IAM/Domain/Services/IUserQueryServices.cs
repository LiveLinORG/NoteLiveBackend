using NoteLiveBackend.IAM.Domain.Model.Aggregates;
using NoteLiveBackend.IAM.Domain.Model.Queries;

namespace NoteLiveBackend.IAM.Domain.Services;

public interface IUserQueryServices
{
    Task<User?> Handle(GetUserByIdQuery query);
    Task<IEnumerable<User>> Handle(GetAllUsersQuery query);
    Task<User?> Handle(GetUserByNameQuery query);
    Task<User?> Handle(GetUserInformationByUsernameQuery query);


}