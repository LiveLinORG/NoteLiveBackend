using NoteLiveBackend.IAM.Domain.Model.Aggregates;
using NoteLiveBackend.IAM.Interfaces.Resources;

namespace NoteLiveBackend.IAM.Interfaces.Transform;

public class UserInformationResourceFromEntityAssembler
{
    public static UserInformationResource ToResourceFromEntity(User user)
    {
        return new UserInformationResource(user.Id, user.Username,user.PasswordHash,user.Role,user.FirstName,user.LastName,user.Email);
    }   
}