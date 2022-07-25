using System.Threading.Tasks;
using VinaCent.Blaze.Models.TokenAuth;
using VinaCent.Blaze.Web.Controllers;
using Shouldly;
using Xunit;

namespace VinaCent.Blaze.Web.Tests.Controllers
{
    public class HomeController_Tests: BlazeWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}