using Com.DanLiris.Service.Core.Lib;
using Com.DanLiris.Service.Core.Lib.Helpers.IdentityService;
using Com.DanLiris.Service.Core.Lib.Services.AccountingCategory;
using Com.DanLiris.Service.Core.Test.DataUtils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Primitives;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.DanLiris.Service.Core.Test.Services.AccountingCategory
{
   public class AccountingCategoryServiceTest
    {
        private const string ENTITY = "GarmentCourier";

        private string GetCurrentAsyncMethod([CallerMemberName] string methodName = "")
        {
            var method = new StackTrace()
                .GetFrames()
                .Select(frame => frame.GetMethod())
                .FirstOrDefault(item => item.Name == methodName);

            return method.Name;

        }

        private CoreDbContext _dbContext(string testName)
        {
            DbContextOptionsBuilder<CoreDbContext> optionsBuilder = new DbContextOptionsBuilder<CoreDbContext>();
            optionsBuilder
                .UseInMemoryDatabase(testName)
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));

            CoreDbContext dbContext = new CoreDbContext(optionsBuilder.Options);

            return dbContext;
        }

        private AccountingCategoryDataUtil _dataUtil(AccountingCategoryService service)
        {
            return new AccountingCategoryDataUtil(service);
        }

        private Mock<IServiceProvider> GetServiceProvider()
        {
            var serviceProvider = new Mock<IServiceProvider>();

            serviceProvider
                .Setup(x => x.GetService(typeof(IIdentityService)))
                .Returns(new IdentityService() { Token = "Token", Username = "Test", TimezoneOffset = 7 });

            return serviceProvider;
        }

        [Fact]
        public async void Should_Success_Create_Data()
        {
            var dbContext = _dbContext(GetCurrentAsyncMethod());
            var serviceProviderMock = GetServiceProvider();
            serviceProviderMock
                .Setup(s => s.GetService(typeof(CoreDbContext)))
                .Returns(dbContext);

            var service = new AccountingCategoryService(serviceProviderMock.Object);
            var data = _dataUtil(service).GetNewData();

            var Response = await service.CreateModel(data);
            Assert.NotEqual(0, Response);
        }

        [Fact]
        public async Task Should_Success_Delete_Data()
        {
            var dbContext = _dbContext(GetCurrentAsyncMethod());
            var serviceProviderMock = GetServiceProvider();
            serviceProviderMock
                .Setup(s => s.GetService(typeof(CoreDbContext)))
                .Returns(dbContext);

            var service = new AccountingCategoryService(serviceProviderMock.Object);
            var data =await _dataUtil(service).GetTestData();

            var Response = await service.DeleteModel(data.Id);
            Assert.NotEqual(0, Response);
        }

        [Fact]
        public async Task Should_Success_ReadModelById()
        {
            var dbContext = _dbContext(GetCurrentAsyncMethod());
            var serviceProviderMock = GetServiceProvider();
            serviceProviderMock
                .Setup(s => s.GetService(typeof(CoreDbContext)))
                .Returns(dbContext);

            var service = new AccountingCategoryService(serviceProviderMock.Object);
            var data = await _dataUtil(service).GetTestData();

            var Response = await service.ReadModelById(data.Id);
            Assert.NotEqual(0, Response.Id);
        }

        [Fact]
        public async void Should_Success_ReadModel()
        {
            var dbContext = _dbContext(GetCurrentAsyncMethod());
            var serviceProviderMock = GetServiceProvider();
            serviceProviderMock
                .Setup(s => s.GetService(typeof(CoreDbContext)))
                .Returns(dbContext);

            var service = new AccountingCategoryService(serviceProviderMock.Object);
            var data = await _dataUtil(service).GetTestData();

            var Response =  service.ReadModel();
            Assert.NotEqual(0, Response.Data.Count);
        }

        [Fact]
        public async void Should_Success_UpdateModel()
        {
            var dbContext = _dbContext(GetCurrentAsyncMethod());
            var serviceProviderMock = GetServiceProvider();
            serviceProviderMock
                .Setup(s => s.GetService(typeof(CoreDbContext)))
                .Returns(dbContext);

            var service = new AccountingCategoryService(serviceProviderMock.Object);
            var data = await _dataUtil(service).GetTestData();
            var newData =  _dataUtil(service).GetNewData();

            var Response = await service.UpdateModel(data.Id, newData);
            Assert.NotEqual(0, Response);
        }

        [Fact]
        public  void Should_Success_UploadValidate()
        {
            var dbContext = _dbContext(GetCurrentAsyncMethod());
            var serviceProviderMock = GetServiceProvider();
            serviceProviderMock
                .Setup(s => s.GetService(typeof(CoreDbContext)))
                .Returns(dbContext);

            var service = new AccountingCategoryService(serviceProviderMock.Object);
           
            List<Lib.Models.AccountingCategory> data = new List<Lib.Models.AccountingCategory>();
            data.Add(new Lib.Models.AccountingCategory());

            List<KeyValuePair<string, StringValues>> body = new List<KeyValuePair<string, StringValues>>();
            var Response =  service.UploadValidate(data, body);
            Assert.NotNull( Response);
        }

        [Fact]
        public void Should_Success_UploadData()
        {
            var dbContext = _dbContext(GetCurrentAsyncMethod());
            var serviceProviderMock = GetServiceProvider();
            serviceProviderMock
                .Setup(s => s.GetService(typeof(CoreDbContext)))
                .Returns(dbContext);

            var service = new AccountingCategoryService(serviceProviderMock.Object);

            List<Lib.Models.AccountingCategory> data = new List<Lib.Models.AccountingCategory>();
            data.Add(new Lib.Models.AccountingCategory());

            var Response = service.UploadData(data);
            Assert.NotNull(Response);
        }
    }
}
