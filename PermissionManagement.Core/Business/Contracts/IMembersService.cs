using Microsoft.AspNetCore.Identity;
using PermissionManagement.Web.Data.ViewModels;
using PermissionManagement.Web.Models;

namespace PermissionManagement.Web.Business.Contracts
{
    public interface IMembersService
    {
        Task<List<IdentityUser>> GetAllMembersAsync(string adminId);
        Task<MembersListVM> GetMembersPermissionsVMAsync();

        Task<List<Page>> GetMembersPagesVMAsync();
    }
}
