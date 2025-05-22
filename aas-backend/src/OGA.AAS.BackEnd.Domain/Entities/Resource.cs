using OGA.Core.BackEnd.Domain.Commons;

namespace OGA.AAS.BackEnd.Domain.Entities
{
    public class Resource : BaseEntity
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public int RootId { get; set; }

        public int? ParentId { get; set; }

        public Resource? Root { get; set; }

        public Resource? Parent { get; set; }

        public IEnumerable<Resource>? Nodes { get; set; }

        public IEnumerable<Resource>? Descendants { get; set; }

        public IEnumerable<Permission>? Permission { get; set; }
    }
}
