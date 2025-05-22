using OGA.Core.BackEnd.Domain.Enums;

namespace OGA.AAS.BackEnd.Domain.Custom;

public class CurrentUserModel
{
    public int UserId { get; set; }
    public string? Email { get; set; }
    public IEnumerable<int>? RoleId { get; set; }
    public string? Token { get; set; }
    public string TimeZoneId { get; set; } = DateTimeEnum.Utc;
}
