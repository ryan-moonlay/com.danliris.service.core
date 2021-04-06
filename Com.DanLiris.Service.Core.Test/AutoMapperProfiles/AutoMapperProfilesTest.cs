using AutoMapper;
using Com.DanLiris.Service.Core.Lib.AutoMapperProfiles;
using Com.DanLiris.Service.Core.Lib.Models;
using Com.DanLiris.Service.Core.Lib.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.DanLiris.Service.Core.Test.AutoMapperProfiles
{
    public class AutoMapperProfilesTest
    {
        [Fact]
        public void Should_Success_GarmentEMKLProfile()
        {
            var mapper = new MapperConfiguration(configuration => configuration.AddProfile<GarmentEMKLProfile>()).CreateMapper();
            var model = new GarmentEMKLModel();
            var vm = mapper.Map<GarmentEMKLViewModel>(model);
            Assert.NotNull(vm);
        }


        [Fact]
        public void Should_Success_GarmentFabricTypeProfile()
        {
            var mapper = new MapperConfiguration(configuration => configuration.AddProfile<GarmentFabricTypeProfile>()).CreateMapper();
            var model = new GarmentFabricTypeModel();
            var vm = mapper.Map<GarmentFabricTypeViewModel>(model);
            Assert.NotNull(vm);
        }


        [Fact]
        public void Should_Success_GarmentForwarderProfile()
        {
            var mapper = new MapperConfiguration(configuration => configuration.AddProfile<GarmentForwarderProfile>()).CreateMapper();
            var model = new GarmentForwarderModel();
            var vm = mapper.Map<GarmentForwarderViewModel>(model);
            Assert.NotNull(vm);
        }

        [Fact]
        public void Should_Success_GarmentLeftoverWarehouseBuyerProfile()
        {
            var mapper = new MapperConfiguration(configuration => configuration.AddProfile<GarmentLeftoverWarehouseBuyerProfile>()).CreateMapper();
            var model = new GarmentLeftoverWarehouseBuyerModel();
            var vm = mapper.Map<GarmentLeftoverWarehouseBuyerViewModel>(model);
            Assert.NotNull(vm);
        }

        [Fact]
        public void Should_Success_GarmentLeftoverWarehouseProductProfile()
        {
            var mapper = new MapperConfiguration(configuration => configuration.AddProfile<GarmentLeftoverWarehouseProductProfile>()).CreateMapper();
            var model = new GarmentLeftoverWarehouseProductModel();
            var vm = mapper.Map<GarmentLeftoverWarehouseProductViewModel>(model);
            Assert.NotNull(vm);
        }

        [Fact]
        public void Should_Success_GarmentLeftoverWarehouseComodityProfile()
        {
            var mapper = new MapperConfiguration(configuration => configuration.AddProfile<GarmentLeftoverWarehouseComodityProfile>()).CreateMapper();
            var model = new GarmentLeftoverWarehouseComodityModel();
            var vm = mapper.Map<GarmentLeftoverWarehouseComodityViewModel>(model);
            Assert.NotNull(vm);
        }

        [Fact]
        public void Should_Success_GarmentShippingStaffProfile()
        {
            var mapper = new MapperConfiguration(configuration => configuration.AddProfile<GarmentShippingStaffProfile>()).CreateMapper();
            var model = new GarmentShippingStaffModel();
            var vm = mapper.Map<GarmentShippingStaffViewModel>(model);
            Assert.NotNull(vm);
        }

        [Fact]
        public void Should_Success_GarmentTransactionTypeProfile()
        {
            var mapper = new MapperConfiguration(configuration => configuration.AddProfile<GarmentTransactionTypeProfile>()).CreateMapper();
            var model = new GarmentTransactionTypeModel();
            var vm = mapper.Map<GarmentTransactionTypeViewModel>(model);
            Assert.NotNull(vm);
        }

        [Fact]
        public void Should_Success_GarmentCourierProfile()
        {
            var mapper = new MapperConfiguration(configuration => configuration.AddProfile<GarmentCourierProfile>()).CreateMapper();
            var model = new GarmentCourierModel();
            var vm = mapper.Map<GarmentCourierViewModel>(model);
            Assert.NotNull(vm);
        }

        [Fact]
        public void Should_Success_GarmentInsuranceProfile()
        {
            var mapper = new MapperConfiguration(configuration => configuration.AddProfile<GarmentInsuranceProfile>()).CreateMapper();
            var model = new GarmentInsuranceModel();
            var vm = mapper.Map<GarmentInsuranceViewModel>(model);
            Assert.NotNull(vm);
        }

        [Fact]
        public void Should_Success_MachineSpinningProfile()
        {
            var mapper = new MapperConfiguration(configuration => configuration.AddProfile<MachineSpinningProfile>()).CreateMapper();
            var machineSpinningModel = new MachineSpinningModel();
            var machineSpinningViewModel = mapper.Map<MachineSpinningViewModel>(machineSpinningModel);
            Assert.NotNull(machineSpinningViewModel);

            var machineSpinningProcessType = new MachineSpinningProcessType();
            var machineSpinningProcessTypeViewModel = mapper.Map<MachineSpinningProcessTypeViewModel>(machineSpinningProcessType);
            Assert.NotNull(machineSpinningProcessTypeViewModel);
        }
    }
}
