using OGA.Core.BackEnd.Domain.Commons;

namespace OGA.AAS.BackEnd.Domain.Models
{
    public class User2FACodeModel : BaseModel
    {
        public int Id { get; set; }

        public int IdUser { get; set; }

        public string? Code { get; set; }

        public string? EndDate { get; set; }
    }
}
