using Microsoft.AspNetCore.Mvc;
using NoteLiveBackend.IAM.Domain.Services;
using NoteLiveBackend.IAM.Interfaces.Resources;
using NoteLiveBackend.IAM.Interfaces.Transform;

namespace NoteLiveBackend.IAM.Interfaces;

public class AuthenticationController(IUserCommandService userCommandService) : ControllerBase
{
    /**
     * <summary>
     *  Endpoint for user sign in
     * </summary>
     * <param name="signInResource">The signIn credentials</param>
     * <returns>The authenticated user</returns>
     */
    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] SignInResource signInResource)
    {
        var signInCommand = SignInCommandFromResourceAssembler.ToCommandFromResource(signInResource);
        var authenticatedUser = await userCommandService.Handle(signInCommand);
        var resource =
            AuthenticatedUserResourceFromEntotyAssembler.ToResourceFromEntity(authenticatedUser.user,
                authenticatedUser.token);
        return Ok(resource);
    }
    
    /**
     * <summary>
     *  Endpoint for user sign up
     * </summary>
     * <param name="signUpResource">The signUp credential</param>
     * <returns>The authenticated user</returns>
     */
    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] SignUpResource signUpResource)
    {
        var signUpCommand = SignUpCommandFromResourceAssembler.ToCommandFromResource(signUpResource);
        await userCommandService.Handle(signUpCommand);
        return Ok(new { message = "User created successfully" });
    }
}