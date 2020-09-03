using Com.DanLiris.Service.Core.Lib;
using Com.DanLiris.Service.Core.Lib.Services;
using Com.DanLiris.Service.Core.Lib.Services.MachineSpinning;
using Com.DanLiris.Service.Core.Test.DataUtils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Xunit;

namespace Com.DanLiris.Service.Core.Test
{
    public class ServiceProviderFixture : IDisposable
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public ServiceProviderFixture()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("Secret", "DANLIRISTESTENVIRONMENT"),
					new KeyValuePair<string, string>("ASPNETCORE_ENVIRONMENT", "Test"),
                    new KeyValuePair<string, string>("DefaultConnection",  "Server=localhost,1401; Database = com.danliris.db.core.service.test; User = sa; password = Standar123.; MultipleActiveResultSets = true; ")
                    //new KeyValuePair<string, string>("DefaultConnection", "Server=(localdb)\\mssqllocaldb;Database=com-danliris-db-test;Trusted_Connection=True;MultipleActiveResultSets=true"),
                   
                })
                .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection") ?? configuration["DefaultConnection"];

            this.ServiceProvider = new ServiceCollection()
                .AddDbContext<CoreDbContext>((serviceProvider, options) =>
                {
                    options.UseSqlServer(connectionString);
                }, ServiceLifetime.Transient)
                .AddTransient<UnitService>(provider => new UnitService(provider) { Username = "TEST" })
                .AddTransient<UnitDataUtil>()
                .AddTransient<OrderTypeService>(provider => new OrderTypeService(provider) { Username = "TEST" })
                .AddTransient<OrderTypeDataUtil>()
                .AddTransient<ProcessTypeService>(provider => new ProcessTypeService(provider) { Username = "TEST" })
                .AddTransient<ProcessTypeDataUtil>()
                .AddTransient<TermOfPaymentService>(provider => new TermOfPaymentService(provider) { Username = "TEST" })
                .AddTransient<TermOfPaymentDataUtil>()
                .AddTransient<HolidayService>(provider => new HolidayService(provider) { Username = "TEST" })
                .AddTransient<HolidayDataUtil>()
                .AddTransient<DesignMotiveService>(provider => new DesignMotiveService(provider) { Username = "TEST" })
                .AddTransient<DesignMotiveDataUtil>()
                .AddTransient<BudgetService>(provider => new BudgetService(provider) { Username = "TEST" })
                .AddTransient<BudgetServiceDataUtil>()
                .AddTransient<ComodityService>(provider => new ComodityService(provider) { Username = "TEST" })
                .AddTransient<ComodityServiceDataUtil>()
                .AddTransient<QualityService>(provider => new QualityService(provider) { Username = "TEST" })
                .AddTransient<QualityServiceDataUtil>()
                .AddTransient<YarnMaterialService>(provider => new YarnMaterialService(provider) { Username = "TEST" })
                .AddTransient<YarnMaterialServiceDataUtil>()
                .AddTransient<MaterialConstructionService>(provider => new MaterialConstructionService(provider) { Username = "TEST" })
                .AddTransient<MaterialConstructionServiceDataUtil>()
                .AddTransient<IncomeTaxService>(provider => new IncomeTaxService(provider) { Username = "TEST" })
                .AddTransient<IncomeTaxDataUtil>()
                .AddTransient<LampStandardService>(provider => new LampStandardService(provider) { Username = "TEST" })
                .AddTransient<LampStandardDataUtil>()
                .AddTransient<StandardTestsService>(provider => new StandardTestsService(provider) { Username = "TEST" })
                .AddTransient<StandardTestDataUtil>()
                .AddTransient<DivisionService>(provider => new DivisionService(provider) { Username = "TEST" })
                .AddTransient<DivisionDataUtil>()
                .AddTransient<ProductService>(provider => new ProductService(provider) { Username = "TEST" })
                .AddTransient<ProductServiceDataUtil>()
                .AddTransient<AccountBankDataUtil>()
                .AddTransient<AccountBankService>(provider => new AccountBankService(provider) { Username = "TEST" })
                .AddTransient<GarmentProductService>(provider => new GarmentProductService(provider) { Username = "TEST" })
                .AddTransient<GarmentProductServiceDataUtil>()
                .AddTransient<GarmentCategoryDataUtil>()
                .AddTransient<GarmentCategoryService>(provider => new GarmentCategoryService(provider) { Username = "TEST" })
                .AddTransient<GarmentBuyerService>(provider => new GarmentBuyerService(provider) { Username = "TEST" })
                .AddTransient<GarmentComodityService>(provider => new GarmentComodityService(provider) { Username = "TEST" })
                .AddTransient<GarmentComodityDataUtil>()
                .AddTransient<GarmentSectionService>(provider => new GarmentSectionService(provider) { Username = "TEST" })
                .AddTransient<StandardMinuteValueService>(provider => new StandardMinuteValueService(provider) { Username = "TEST" })
                .AddTransient<StandardMinuteValueDataUtil>()
                .AddTransient<GarmentSupplierDataUtil>()
                .AddTransient<GarmentBuyerBrandDataUtil>()
                .AddTransient<GarmentBuyerDataUtil>()
                .AddTransient<GarmentSupplierService>(provider => new GarmentSupplierService(provider) { Username = "TEST" })
				.AddTransient<GarmentUnitService>(provider => new GarmentUnitService(provider) { Username = "TEST" })
                .AddTransient<GarmentBuyerBrandService>(provider => new GarmentBuyerBrandService(provider) { Username = "TEST" })
                .AddTransient<UnitService>(provider => new UnitService(provider) { Username = "TEST" })
				.AddTransient<GarmentCurrencyService>(provider => new GarmentCurrencyService(provider) { Username = "TEST" })
				.AddTransient<GarmentCurrencyDataUtil>()
				.AddTransient<BudgetCurrencyService>(provider => new BudgetCurrencyService(provider) { Username = "TEST" })
				.AddTransient<BudgetCurrencyDataUtil>()
                .AddTransient<UomService>(provider => new UomService(provider) { Username = "TEST" })
                .AddTransient<UomServiceDataUtil>()
                .AddTransient<MachineSpinningService>(provider => new MachineSpinningService(provider))
                .AddTransient<MachineSpinningDataUtil>()
                .AddTransient<SizeService>(provider => new SizeService(provider) { Username = "TEST" })
                .AddTransient<SizeDataUtil>()
                .AddTransient<AccountRoleDataUtil>()
                .AddTransient<PermissionDataUtil>()
                .AddTransient(provider => new StorageService(provider) { Username = "TEST" })
                .AddTransient(provider => new BuyerService(provider) { Username = "TEST" })
                .AddTransient(provider => new CategoryService(provider) { Username = "TEST" })
                .AddTransient(provider => new CurrencyService(provider) { Username = "TEST" })
                .AddTransient(provider => new SupplierService(provider) { Username = "TEST" })

                .BuildServiceProvider();

            CoreDbContext dbContext = ServiceProvider.GetService<CoreDbContext>();
            dbContext.Database.Migrate();
        }

        public void Dispose()
        {
        }
    }

    [CollectionDefinition("ServiceProviderFixture Collection")]
    public class ServiceProviderFixtureCollection : ICollectionFixture<ServiceProviderFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}