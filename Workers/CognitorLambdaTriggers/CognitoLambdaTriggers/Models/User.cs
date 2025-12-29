

namespace CognitoLambdaTriggers.Models;

public class User
{
    public string UserId { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;

    // for EF core context
    public User() { }

    public User(string userId, string email, string givenName, string familyName) : this()
    {
        UserId = userId;
        Email = email;
        FirstName = givenName;
        LastName = familyName;

    }
}