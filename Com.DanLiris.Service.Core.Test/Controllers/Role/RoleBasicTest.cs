using Com.DanLiris.Service.Core.Lib.ViewModels.Account_and_Roles;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.DanLiris.Service.Core.Test.Controllers.Role
{
    [Collection("TestFixture Collection")]
   public class RoleBasicTest
    {
        private const string URI = "v1/roles";

        protected TestServerFixture TestFixture { get; set; }

        protected HttpClient Client
        {
            get { return this.TestFixture.Client; }
        }

        public RoleBasicTest(TestServerFixture fixture)
        {
            TestFixture = fixture;

        }

        public RoleViewModel GenerateTestModel()
        {
            string guid = Guid.NewGuid().ToString();

            return new RoleViewModel()
            {
                Code = guid,
                Name = string.Format("TEST {0}", guid),
                Description = "Description",
                Permissions =new List<PermissionViewModel>()
                {
                    new PermissionViewModel()
                    {
                        unit =new Lib.ViewModels.UnitViewModel()
                    }
                }
            };
        }

        [Fact]
        public async Task Get()
        {
            var response = await this.Client.GetAsync(URI);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }


        [Fact]
        public async Task GetById()
        {
            var response = await this.Client.GetAsync(string.Concat(URI, "/"));
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Post()
        {

            RoleViewModel VM = GenerateTestModel();
            var response = await this.Client.PostAsync(URI, new StringContent(JsonConvert.SerializeObject(VM).ToString(), Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }
}
