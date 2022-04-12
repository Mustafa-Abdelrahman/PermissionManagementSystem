using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PermissionManagement.Web.Business.Contracts;
using PermissionManagement.Web.Business.Services;
using PermissionManagement.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionManagementSystem.Tests.Business.Services
{
    [TestClass]
    public class UserServiceTests
    {
        private readonly UserService _sut ;
        private readonly Mock<ApplicationDbContext> _context ;
        public UserServiceTests()
        {
            _sut = new UserService();
        }

        [TestMethod]
        public void GetUserAsync_ShouldReturnnull()
        {
           var user =  _sut.GetLoggedUserAsync().Result;
            Assert.IsNull(user);
        }
    }
}
