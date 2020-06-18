using Com.Moonlay.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Com.DanLiris.Service.Core.Lib.Models;
using Com.DanLiris.Service.Core.Lib.Models.Account_and_Roles;

namespace Com.DanLiris.Service.Core.Lib
{
    public class CoreDbContext : BaseDbContext
    {
        public CoreDbContext(DbContextOptions<CoreDbContext> options) : base(options)
        {
        }

        public DbSet<AccountBank> AccountBanks { get; set; }

        public DbSet<Budget> Budgets { get; set; }

		public DbSet<BudgetCurrency> BudgetCurrencies { get; set; }

		public DbSet<Buyer> Buyers { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Currency> Currencies { get; set; }

        public DbSet<Division> Divisions { get; set; }

        public DbSet<GarmentCurrency> GarmentCurrencies { get; set; }

        public DbSet<GarmentBuyer> GarmentBuyers { get; set; }

        public DbSet<GarmentComodity> GarmentComodities { get; set; }

        public DbSet<Holiday> Holidays { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Storage> Storages { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<TermOfPayment> TermOfPayments { get; set; }

        public DbSet<Unit> Units { get; set; }

        public DbSet<Uom> UnitOfMeasurements { get; set; }
        public DbSet<OrderType> OrderTypes { get; set; }

        public DbSet<IncomeTax> IncomeTaxes { get; set; }

        public DbSet<Quality> Qualities { get; set; }
        public DbSet<Comodity> Comodities { get; set; }
        public DbSet<YarnMaterial> YarnMaterials { get; set; }
        public DbSet<MaterialConstruction> MaterialConstructions { get; set; }
        public DbSet<DesignMotive> DesignMotives { get; set; }
        public DbSet<ProcessType> ProcessType { get; set; }
        public DbSet<FinishType> FinishType { get; set; }
        public DbSet<StandardTests> StandardTests { get; set; }
        public DbSet<AccountProfile> AccountProfiles { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<LampStandard> LampStandard { get; set; }
        public DbSet<ColorTypes> ColorTypes { get; set; }
        public DbSet<GarmentProduct> GarmentProducts { get; set; }
        public DbSet<GarmentCategory> GarmentCategories { get; set; }
		public DbSet<GarmentSupplier> GarmentSuppliers { get; set; }
        public DbSet<GarmentBuyerBrand> GarmentBuyerBrands { get; set; }
        public DbSet<ProductSPPProperty> ProductSPPProperties { get; set; }
        public DbSet<MachineSpinningModel> MachineSpinnings { get; set; }
        public DbSet<GarmentSection> GarmentSections { get; set; }
        public DbSet<StandardMinuteValue> StandardMinuteValues { get; set; }
        public DbSet<MachineSpinningProcessType> MachineSpinningProcessType { get; set; }
        public DbSet<MappingCategory> MappingCategories { get; set; }
        public DbSet<SizeModel> Sizes { get; set; }
        public DbSet<GarmentLeftoverWarehouseBuyerModel> GarmentLeftoverWarehouseBuyers { get; set; }
        public DbSet<GarmentShippingStaffModel> GarmentShippingStaffs { get; set; }
        public DbSet<GarmentFabricTypeModel> GarmentFabricTypes { get; set; }
        public DbSet<GarmentEMKLModel> GarmentEMKLs { get; set; }
        public DbSet<GarmentForwarderModel> GarmentForwarders { get; set; }
        public DbSet<GarmentTransactionTypeModel> GarmentTransactionTypes { get; set; }
        public DbSet<GarmentLeftoverWarehouseProductModel> GarmentLeftoverWarehouseProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Buyer>()
                .HasIndex(b => b.Code);

            modelBuilder.Entity<MappingCategory>()
                .HasKey(x => x.Id);
        }
    }
}
