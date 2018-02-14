using NUnit;
using NUnit.Framework;
using NSubstitute;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WattsCRM.Controllers;
using Moq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WattsCRM.Models;

namespace WattsCRM.UnitTests
{
    [TestFixture]
    public class WattsControllerTests
    {
        WattsCRM.Interfaces.IApplicationDbContext _dbContext;
        WattsCRM.Models.ClientViewModel _clientViewModel;
        WattsCRM.Interfaces.IClientRepository _repository;

        [SetUp]
        public void Setup()
        {

        }

    //    [Test]
    //    [Category("ControllerTests")]
    //    public void EditControllerEmptyId()
    //    {
    //        this._dbContext = Substitute.For<WattsCRM.Interfaces.IApplicationDbContext>();
    //        this._repository = Substitute.For<WattsCRM.Interfaces.IClientRepository>();

    //        //this._repository.AddClient(GetClientViewModel1(), "FE19D05F42044440B8C82E0A825E9102");

    //        var mockUserStore = new Mock<Microsoft.AspNetCore.Identity.IUserStore<Models.ApplicationUser>>();
    //        var userManager = new UserManager<Models.ApplicationUser>(mockUserStore.Object);
    //        //var userManager = new Mock<UserManager<Models.ApplicationUser>>(mockUserStore.Object, null, null, null, null, null, null, null, null);


    //        var user = new ApplicationUser()
    //        {
    //            Id = "FE19D05F42044440B8C82E0A825E9102",
    //            Email = "user@gmail.com",
    //            ConcurrencyStamp = "true",
    //            LockoutEnabled = false,
    //            NormalizedEmail = "user@gmail.com",
    //            PhoneNumber = "555-555-5555",
    //            SecurityStamp = "",
    //            EmailConfirmed = true,
    //            NormalizedUserName = "user",
    //            TwoFactorEnabled = false,
    //            UserName = "user",
    //            AccessFailedCount = 0
    //        };

    //        var user2 = new System.Security.Claims.ClaimsPrincipal();

    //        HttpContext httpContext =  new DefaultHttpContext();

    //        httpContext.Request.Headers["device-id"] = "20317";
    //        httpContext.User = user2;


    //        //private HttpRequest rmRequest;
    //        //private Mock<HttpContext> moqContext;
    //        //private Mock<HttpRequest> moqRequest;

    //        userManager.Returns(

    //        var controller = new WattsCRM.Controllers.ClientController(userManager.Object, this._dbContext, this._repository, httpContext);

    //    var edit = controller.Edit(new Guid(), GetClientViewModel1());

    //    //Assert.IsTrue(edit.ToString() == "404");
    //}


        private WattsCRM.Models.ClientViewModel GetClientViewModel1()
        {
            return _clientViewModel = new Models.ClientViewModel
            {
                Address = "123"
            };
        }

    }
}