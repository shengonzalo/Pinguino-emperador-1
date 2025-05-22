﻿using OGA.Core.BackEnd.Domain.Commons;
using Swashbuckle.AspNetCore.Annotations;

namespace OGA.AAS.BackEnd.Domain.Models
{
    public class RoleModel : BaseModel
    {
        public int Id { get; set; }

        [SwaggerSchema(Description = "Maximum length: 256")]
        public string? Name { get; set; }

        [SwaggerSchema(Description = "Maximum length: 256")]        
        public string? Description { get; set; }
        public int? Order { get; set; }
    }
}
