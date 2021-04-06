using Com.DanLiris.Service.Core.Lib;
using Com.DanLiris.Service.Core.Lib.Helpers.IdentityService;
using Com.DanLiris.Service.Core.Lib.Services.GarmentLeftoverWarehouseComodity;
using Com.DanLiris.Service.Core.Lib.ViewModels;
using Com.DanLiris.Service.Core.Test.DataUtils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Xunit;

namespace Com.DanLiris.Service.Core.Test.Services.GarmentLeftoverWarehouseComodity
{
    public class BasicTest
    {
        private const string ENTITY = "GarmentLeftoverWarehouseComodity";

        [MethodImpl(MethodImplOptions.NoInlining)]
        public string GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);

            return string.Concat(sf.GetMethod().Name, "_", ENTITY);
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

        private GarmentLeftoverWarehouseComodityDataUtil _dataUtil(GarmentLeftoverWarehouseComodityService service)
        {
            return new GarmentLeftoverWarehouseComodityDataUtil(service);
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
            var service = new GarmentLeftoverWarehouseComodityService(GetServiceProvider().Object, _dbContext(GetCurrentMethod()));
            var data = _dataUtil(service).GetNewData();

            var Response = await service.CreateAsync(data);
            Assert.NotEqual(0, Response);
        }

        [Fact]
        public async void Should_Success_Get_Data()
        {
            var service = new GarmentLeftoverWarehouseComodityService(GetServiceProvider().Object, _dbContext(GetCurrentMethod()));
            var data = await _dataUtil(service).GetTestData();

            var Response = service.Read(1, 25, "{}", null, data.Name, "{}");
            Assert.NotEmpty(Response.Data);
        }

        [Fact]
        public async void Should_Success_Get_Data_By_Id()
        {
            var service = new GarmentLeftoverWarehouseComodityService(GetServiceProvider().Object, _dbContext(GetCurrentMethod()));
            var data = await _dataUtil(service).GetTestData();

            var Response = await service.ReadByIdAsync(data.Id);
            Assert.NotNull(Response);
        }

        [Fact]
        public async void Should_Success_Update_Data()
        {
            var service = new GarmentLeftoverWarehouseComodityService(GetServiceProvider().Object, _dbContext(GetCurrentMethod()));
            var data = await _dataUtil(service).GetTestData();
            var newData = await service.ReadByIdAsync(data.Id);

            var Response = await service.UpdateAsync(newData.Id, newData);
            Assert.NotEqual(0, Response);
        }

        [Fact]
        public async void Should_Success_Delete_Data()
        {
            var service = new GarmentLeftoverWarehouseComodityService(GetServiceProvider().Object, _dbContext(GetCurrentMethod()));
            var data = await _dataUtil(service).GetTestData();

            var Response = await service.DeleteAsync(data.Id);
            Assert.NotEqual(0, Response);
        }

        [Fact]
        public void Validate_ViewModel()
        {
            var serviceProvider = GetServiceProvider();

            var viewModel = new GarmentLeftoverWarehouseComodityViewModel();
            var validationContext = new ValidationContext(viewModel, serviceProvider.Object, null);

            var result = viewModel.Validate(validationContext);

            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public async void Validate_ViewModel_Duplicate()
        {
            var serviceProvider = GetServiceProvider();

            var service = new GarmentLeftoverWarehouseComodityService(serviceProvider.Object, _dbContext(GetCurrentMethod()));
            serviceProvider.Setup(s => s.GetService(typeof(IGarmentLeftoverWarehouseComodityService)))
                .Returns(service);

            var data = await _dataUtil(service).GetTestData();
            var viewModel = new GarmentLeftoverWarehouseComodityViewModel
            {
                Code = data.Code,
                Name = data.Name,
            };

            var validationContext = new ValidationContext(viewModel, serviceProvider.Object, null);

            var result = viewModel.Validate(validationContext);

            Assert.NotEmpty(result.ToList());
        }
    }
}
