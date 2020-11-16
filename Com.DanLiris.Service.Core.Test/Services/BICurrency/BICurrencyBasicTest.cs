using Com.DanLiris.Service.Core.Lib;
using Com.DanLiris.Service.Core.Lib.Helpers;
using Com.DanLiris.Service.Core.Lib.Helpers.IdentityService;
using Com.DanLiris.Service.Core.Lib.Services.BICurrency;
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

namespace Com.DanLiris.Service.Core.Test.Services.BICurrency
{
  public  class BICurrencyBasicTest
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

        private BICurrencyDataUtil _dataUtil(BICurrencyService service)
        {
            return new BICurrencyDataUtil(service);
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

            var service = new BICurrencyService(serviceProviderMock.Object);
            var data = _dataUtil(service).GetNewData();

            var Response = await service.CreateModel(data);
            Assert.NotEqual(0, Response);
        }

        [Fact]
        public async void Should_Duplicate_Fail_When_Create_Data()
        {
            var dbContext = _dbContext(GetCurrentAsyncMethod());
            var serviceProviderMock = GetServiceProvider();
            serviceProviderMock
                .Setup(s => s.GetService(typeof(CoreDbContext)))
                .Returns(dbContext);

            var service = new BICurrencyService(serviceProviderMock.Object);
            var data =await _dataUtil(service).GetTestData();
            var newData = _dataUtil(service).GetNewData();

            await Assert.ThrowsAsync<ServiceValidationException>(() => service.CreateModel(newData));


        }

        [Fact]
        public async void Should_Success_Delete_Data()
        {
            var dbContext = _dbContext(GetCurrentAsyncMethod());
            var serviceProviderMock = GetServiceProvider();
            serviceProviderMock
                .Setup(s => s.GetService(typeof(CoreDbContext)))
                .Returns(dbContext);

            var service = new BICurrencyService(serviceProviderMock.Object);
            var data =await _dataUtil(service).GetTestData();

            var Response = await service.DeleteModel(data.Id);
            Assert.NotEqual(0, Response);
        }

        [Fact]
        public async void Should_Success_ReadModel()
        {
            var dbContext = _dbContext(GetCurrentAsyncMethod());
            var serviceProviderMock = GetServiceProvider();
            serviceProviderMock
                .Setup(s => s.GetService(typeof(CoreDbContext)))
                .Returns(dbContext);

            var service = new BICurrencyService(serviceProviderMock.Object);
            var data = await _dataUtil(service).GetTestData();

            var Response =  service.ReadModel(data.Id);
            Assert.NotNull(Response);
        }

        [Fact]
        public async void Should_Success_ReadModelById()
        {
            var dbContext = _dbContext(GetCurrentAsyncMethod());
            var serviceProviderMock = GetServiceProvider();
            serviceProviderMock
                .Setup(s => s.GetService(typeof(CoreDbContext)))
                .Returns(dbContext);

            var service = new BICurrencyService(serviceProviderMock.Object);
            var data = await _dataUtil(service).GetTestData();

            var Response = service.ReadModelById(data.Id);
            Assert.NotNull(Response);
        }

        [Fact]
        public async Task Should_Success_UpdateModel()
        {
            var dbContext = _dbContext(GetCurrentAsyncMethod());
            var serviceProviderMock = GetServiceProvider();
            serviceProviderMock
                .Setup(s => s.GetService(typeof(CoreDbContext)))
                .Returns(dbContext);

            var service = new BICurrencyService(serviceProviderMock.Object);
            var data = await _dataUtil(service).GetTestData();
            var newData = _dataUtil(service).GetNewData();
            newData.Code = "idr";
            newData.Name = "rupiah";
            newData.Date = DateTime.Now.AddDays(1);

            var Response = service.UpdateModel(data.Id, newData);
            Assert.NotNull(Response);
        }

        [Fact]
        public async Task Should_fail_UpdateModel()
        {
            var dbContext = _dbContext(GetCurrentAsyncMethod());
            var serviceProviderMock = GetServiceProvider();
            serviceProviderMock
                .Setup(s => s.GetService(typeof(CoreDbContext)))
                .Returns(dbContext);

            var service = new BICurrencyService(serviceProviderMock.Object);
            var data = await _dataUtil(service).GetTestData();
            var newData = _dataUtil(service).GetNewData();
            

            await Assert.ThrowsAsync<ServiceValidationException>(() => service.UpdateModel(data.Id, newData));
        }

        [Fact]
        public async Task Should_Success_UploadData()
        {
            var dbContext = _dbContext(GetCurrentAsyncMethod());
            var serviceProviderMock = GetServiceProvider();
            serviceProviderMock
                .Setup(s => s.GetService(typeof(CoreDbContext)))
                .Returns(dbContext);

            var service = new BICurrencyService(serviceProviderMock.Object);
            
            var newData = _dataUtil(service).GetNewData();

            List<Lib.Models.BICurrency> currencies = new List<Lib.Models.BICurrency>();
            currencies.Add(newData);

            var Response =await service.UploadData(currencies);
            Assert.NotNull(Response);
        }

        [Fact]
        public  void Should_Success_UploadValidate()
        {
            var dbContext = _dbContext(GetCurrentAsyncMethod());
            var serviceProviderMock = GetServiceProvider();
            serviceProviderMock
                .Setup(s => s.GetService(typeof(CoreDbContext)))
                .Returns(dbContext);

            var service = new BICurrencyService(serviceProviderMock.Object);

            List<Lib.Models.BICurrency> currencies = new List<Lib.Models.BICurrency>();
            currencies.Add(new Lib.Models.BICurrency());

            List<KeyValuePair<string, StringValues>> body = new List<KeyValuePair<string, StringValues>>();
            var Response =  service.UploadValidate(currencies, body);
            Assert.NotNull(Response);
        }

    }
}
