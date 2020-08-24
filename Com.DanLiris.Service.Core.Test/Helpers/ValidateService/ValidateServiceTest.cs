using Com.DanLiris.Service.Core.Lib.Helpers;
using Com.DanLiris.Service.Core.Lib.ViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.DanLiris.Service.Core.Test.Helpers.ValidateService
{
 public   class ValidateServiceTest
    {

        [Fact]
        public void Should_Succes_Instantiate_validateService()
        {
            var serviceProviderMock = new Mock<IServiceProvider>();
            var service = new Lib.Helpers.ValidateService.ValidateService(serviceProviderMock.Object);

            AccountBankViewModel viewModel = new AccountBankViewModel();
            service.Validate(viewModel);

            Assert.NotNull(service);
        }



        [Fact]
        public void Validate_Throws_ServiceValidationExeption()
        {
            Mock<IServiceProvider> serviceProvider = new Mock<IServiceProvider>();
            MachineSpinningProcessTypeViewModel viewModel = new MachineSpinningProcessTypeViewModel();

            var service = new Lib.Helpers.ValidateService.ValidateService(serviceProvider.Object);
            Assert.Throws<ServiceValidationException>(() => service.Validate(viewModel));

        }
    }
}
