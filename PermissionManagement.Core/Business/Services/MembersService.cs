using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PermissionManagement.Web.Business.Contracts;
using PermissionManagement.Web.Data;
using PermissionManagement.Web.Data.Constants;
using PermissionManagement.Web.Data.ViewModels;
using PermissionManagement.Web.Models;

namespace PermissionManagement.Web.Business.Services
{
    public class MembersService: IMembersService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IUserService userService;

        public MembersService(ApplicationDbContext dbContext, IUserService userService)
        {
            this.dbContext = dbContext;
            this.userService = userService;
        }

        public async Task<List<IdentityUser>> GetAllMembersAsync(string adminId)
        {
           return await userService.GetUsersInRoleAsync(Roles.Member.ToString());
        }

        public async Task<MembersListVM> GetMembersPermissionsVMAsync()
        {
            var allMembers = await this.GetAllMembersAsync(userService.GetLoggedInUserAsync().Result.Id); // assuming the logged in User is an admin
            return new MembersListVM { AllMembers = allMembers, SelectedUserId = string.Empty };
        }

        public async Task<List<Page>> GetMembersPagesVMAsync()
        {
            return await dbContext.Pages.Where(p => p.AssociatedRole == Roles.Member.ToString()).Include(pb => pb.PageBlocks).ToListAsync();
        }
    }
}
