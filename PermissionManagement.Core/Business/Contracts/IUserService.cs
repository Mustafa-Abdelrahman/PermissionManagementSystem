using Microsoft.AspNetCore.Identity;
using PermissionManagement.Web.Data.ViewModels;
using System.Security.Claims;

namespace PermissionManagement.Web.Business.Contracts
{
    public interface IUserService
    {
        Task<IdentityUser> GetLoggedInUserAsync();
        Task<List<string>> GetUserClaimsStringValuesAsync(string id ="");
        Task<IdentityRole> GetUserRoleAsync(string Id);

        Task<UserAuthorizationVM> GetUserAuthorizationVM(string Id);
        Task SaveClaimsAsync(UserAuthorizationVM editedPermissionsVM);
    }
}
