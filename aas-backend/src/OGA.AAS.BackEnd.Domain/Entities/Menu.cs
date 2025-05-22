using OGA.Core.BackEnd.Domain.Commons;

namespace OGA.AAS.BackEnd.Domain.Entities
{
    public class Menu : BaseEntity
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Icon { get; set; }

        public int RootId { get; set; }

        public int? ParentId { get; set; }

        public string? Path { get; set; }

        public int Order { get; set; }

        public string? ElementId { get; set; }
    }
}
