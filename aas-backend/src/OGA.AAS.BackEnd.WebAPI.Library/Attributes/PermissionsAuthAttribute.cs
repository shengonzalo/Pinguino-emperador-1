using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using OGA.AAS.BackEnd.Domain.Contracts.Services;
using OGA.AAS.BackEnd.WebAPI.Library.Resources;
using OGA.Core.BackEnd.Domain.Exceptions;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace OGA.AAS.BackEnd.WebAPI.Library.Attributes
{
    [ExcludeFromCodeCoverage]
    public class PermissionsAuthAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly int _resourceId;
        private readonly int[] _permissions;

        public PermissionsAuthAttribute(int resourceId, int[] permissions) : base()
        {
            _resourceId = resourceId;
            _permissions = permissions;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                var email = context.HttpContext.User.FindFirstValue(ClaimTypes.Email);
                var permissionsAux = _permissions;

                if (context.HttpContext.RequestServices.GetService(typeof(IPermissionService)) is IPermissionService permissionService)
                {
                    var userPerm = permissionService.GetUserPermissions(_resourceId, email).Result;

                    if (userPerm != null && userPerm.Permissions != null)
                    {
                        var permission = userPerm.Permissions.FirstOrDefault(x => permissionsAux.Contains(x.Id) && x.Allowed);

                        if (permission != null)
                        {
                            permissionsAux = permissionsAux.Where(x => x != permission.Id).ToArray();

                            if (permissionsAux.Any() && userPerm.Nodes != null)
                            {
                                var hasPerm = false;

                                foreach (var node in userPerm.Nodes)
                                {
                                    if (permissionsAux.Any() && node.Permissions != null)
                                    {
                                        permission = node.Permissions.FirstOrDefault(x => permissionsAux.Contains(x.Id) && x.Allowed);

                                        if (permission != null)
                                        {
                                            hasPerm = true;
                                            permissionsAux = permissionsAux.Where(x => x != permission.Id).ToArray();
                                        }
                                    }
                                }

                                if (hasPerm)
                                {
                                    return;
                                }
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                }

                throw new UnauthorizedException(PermissionsAuthMessages.UnauthorizedException);
            }
            catch (Exception e)
            {
                throw new UnauthorizedException(e.Message);
            }
        }
    }
}
