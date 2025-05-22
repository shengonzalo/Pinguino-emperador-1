using OGA.AAS.BackEnd.Domain.Models;
using Swashbuckle.AspNetCore.Filters;

namespace OGA.AAS.BackEnd.WebAPI.Controllers.Swagger
{
    public class UserPermissionModelExample : IExamplesProvider<UserPermissionModel>
    {
        public UserPermissionModel GetExamples()
        {
            return new UserPermissionModel()
            {
                Resource = new ResourcePermissionModel()
                {
                    Id = 1,
                    Name = "Pantalla 1",
                    Description = "Descripción de la pantalla 1"
                },
                Permissions = new List<PermissionModel>()
                {
                    new PermissionModel()
                    {
                        Id = 1,
                        Name = "Permiso 1",
                        Description = "Descripción del permiso 1",
                        Allowed = true,
                        PermissionType = new PermissionTypeModel()
                        {
                            Id = 1,
                            Description = "Descripción del tipo de permiso 1"
                        }
                    }
                }
            };
        }
    }
}
