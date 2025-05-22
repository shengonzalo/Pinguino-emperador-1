namespace OGA.AAS.BackEnd.Domain.Custom;

public class CurrentUserProvider
{
    private CurrentUserModel? CurrentUser;

    public CurrentUserModel? GetCurrentUser()
    {
        return CurrentUser;
    }

    public void SetCurrentUser(string userId, string email, IEnumerable<string> rol, string token, string timeZoneId)
    {
        CurrentUser = new CurrentUserModel()
        {
            UserId = int.Parse(userId),
            Email = email,
            RoleId = rol.Select(x => int.Parse(x)),
            Token = token,
            TimeZoneId = timeZoneId
        };

    }
}
