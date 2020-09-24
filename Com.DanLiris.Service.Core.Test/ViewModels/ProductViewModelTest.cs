using Com.DanLiris.Service.Core.Lib.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.DanLiris.Service.Core.Test.ViewModels
{
     public class ProductViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate_ProductSPPPropertyViewModel()
        {
            ProductSPPPropertyViewModel viewModel = new ProductSPPPropertyViewModel()
            {
                BuyerAddress= "BuyerAddress",
                BuyerId=1,
                BuyerName= "BuyerName",
                ColorName= "ColorName",
                Construction= "Construction",
                DesignCode= "DesignCode",
                DesignNumber= "DesignNumber",
                Grade="A",
                Length=1,
                Lot= "Lot",
                OrderType=new OrderTypeViewModel()
                {
                    Code= "Code",
                    Name= "Name",
                    Remark= "Remark"
                },
                ProductionOrderId=1,
               ProductionOrderNo= "ProductionOrderNo",
               Weight=1
            };

            Assert.Equal("BuyerAddress", viewModel.BuyerAddress);
            Assert.Equal(1, viewModel.BuyerId);
            Assert.Equal("BuyerName", viewModel.BuyerName);
            Assert.Equal("ColorName", viewModel.ColorName);
            Assert.Equal("Construction", viewModel.Construction);
            Assert.Equal("DesignCode", viewModel.DesignCode);
            Assert.Equal("DesignNumber", viewModel.DesignNumber);
            Assert.Equal("A", viewModel.Grade);
            Assert.Equal(1, viewModel.Length);
            Assert.Equal("Lot", viewModel.Lot);
            Assert.NotNull(viewModel.OrderType);
            Assert.Equal(1, viewModel.ProductionOrderId);
            Assert.Equal("ProductionOrderNo", viewModel.ProductionOrderNo);
            Assert.Equal(1, viewModel.Weight);
        }

        [Fact]
        public void Should_Success_Instantiate_ProductDesignViewModel()
        {
            ProductDesignViewModel viewModel = new ProductDesignViewModel()
            {
                Code = "Code",
                Number = "1"
            };
            Assert.Equal("Code", viewModel.Code);
            Assert.Equal("1", viewModel.Number);
        }
        }
}
