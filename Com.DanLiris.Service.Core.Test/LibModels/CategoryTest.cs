using Com.DanLiris.Service.Core.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.DanLiris.Service.Core.Test.LibModels
{
  public  class CategoryTest
    {
        [Fact]
        public void Validate_PurchasingCOA_by_splitingPoint()
        {
            Category category = new Category()
            {
                PurchasingCOA = "111111"
            };
            var result = category.Validate(null);
            Assert.True(result.Count() > 0);
        }

        [Fact]
        public void Validate_UnValidCode_PurchasingCOA()
        {
            Category category = new Category()
            {
                PurchasingCOA = "111.111"
            };
            var result = category.Validate(null);
            Assert.True(result.Count() > 0);
        }

        [Fact]
        public void Validate_StockCOA_by_splitingPoint()
        {
            Category category = new Category()
            {   
                StockCOA="111111",
            };
            var result = category.Validate(null);
            Assert.True(result.Count() > 0);
        }

        [Fact]
        public void Validate_UnValidCode_StockCOA()
        {
            Category category = new Category()
            {
                StockCOA = "111.111",
            };
            var result = category.Validate(null);
            Assert.True(result.Count() > 0);
        }


        [Fact]
        public void Validate_LocalDebtCOA_by_splitingPoint()
        {
            Category category = new Category()
            {
                LocalDebtCOA = "111111",
            };
            var result = category.Validate(null);
            Assert.True(result.Count() > 0);
        }

        [Fact]
        public void Validate_UnValidCode_LocalDebtCOA()
        {
            Category category = new Category()
            {
                LocalDebtCOA = "111.111",
            };
            var result = category.Validate(null);
            Assert.True(result.Count() > 0);
        }

        [Fact]
        public void Validate_ImportDebtCOA_by_splitingPoint()
        {
            Category category = new Category()
            {
                ImportDebtCOA = "111111",
            };
            var result = category.Validate(null);
            Assert.True(result.Count() > 0);
        }

        [Fact]
        public void Validate_UnValidCode_ImportDebtCOA()
        {
            Category category = new Category()
            {
                ImportDebtCOA = "111.111",
            };
            var result = category.Validate(null);
            Assert.True(result.Count() > 0);
        }

       
    }
}
