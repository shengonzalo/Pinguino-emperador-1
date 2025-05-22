using OGA.Core.BackEnd.Domain.Commons;

namespace OGA.AAS.BackEnd.Domain.Entities
{
    public class User2FACode : BaseEntity
    {
        public int IdUser { get; set; }

        public string? Code { get; set; }
        
        public DateTime EndDate { get; set; }
    }
}
