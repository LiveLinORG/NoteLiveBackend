using NoteLiveBackend.IAM.Domain.Model.Aggregates;
using NoteLiveBackend.IAM.Interfaces.Resources;

namespace NoteLiveBackend.IAM.Interfaces.Transform;

public class AuthenticatedUserResourceFromEntotyAssembler
{
    public static AuthenticatedUserResource ToResourceFromEntity(
        User user, string token)
    {
        return new AuthenticatedUserResource(user.Id, user.Username, token);
    }
}