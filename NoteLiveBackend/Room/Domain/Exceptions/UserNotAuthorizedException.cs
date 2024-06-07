namespace NoteLiveBackend.Room.Domain.Exceptions;

public class UserNotAuthorizedException : Exception
{
    public UserNotAuthorizedException() : base("User not authorized") { }
}
