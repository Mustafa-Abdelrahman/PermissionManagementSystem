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

        public Task<List<IdentityUser>> GetAllMembersAsync(string adminId)
        {
           return  dbContext.Users.Where(u => u.Id != adminId).OrderBy(x => x.UserName).ToListAsync();
        }

        public async Task<MembersListVM> GetMembersPermissionsVMAsync()
        {
            var allMembers = await this.GetAllMembersAsync(userService.GetUserAsync().Result.Id);
            return new MembersListVM { AllMembers = allMembers, SelectedUserId = string.Empty };
        }

        public async Task<List<Page>> GetMembersPagesVMAsync()
        {
            return await dbContext.Pages.Where(p => p.AssociatedRole == Roles.Member.ToString()).Include(pb => pb.PageBlocks).ToListAsync();
        }
    }
}
