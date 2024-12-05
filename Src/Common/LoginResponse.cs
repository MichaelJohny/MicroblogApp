namespace Common;

public class LoginResponse
{
    public LoginResponse(string userId, string token, string userName)
    {
        UserId = userId;
        Token = token;
        UserName = userName;
    }

    public string UserId { get;  }
    public string Token { get;  }
    public string UserName { get; }
}