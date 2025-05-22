
using OGA.Core.BackEnd.Domain.Commons;

namespace OGA.AAS.BackEnd.Domain.Entities
{
    public class Language : BaseEntity
    {
        public string? Code { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }
    }
}
