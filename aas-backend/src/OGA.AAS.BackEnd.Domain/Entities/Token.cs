using OGA.Core.BackEnd.Domain.Commons;

namespace OGA.AAS.BackEnd.Domain.Entities
{
    public class Token : BaseEntity
    {
        public string? AccessToken { get; set; }

        public string? Email { get; set; }

        public DateTime ExpiredDate { get; set; }

        public bool IsRefreshToken { get; set; }
    }
}
