using Com.DanLiris.Service.Core.Lib.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.DanLiris.Service.Core.Test.LibModels
{
   public class MappingCategoryTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            MappingCategory category = new MappingCategory()
            {
                Id=1,
                CategoryId=1,
                DivisionId=1,
                ProductId=1
            };
            Assert.Equal(1, category.Id);
            Assert.Equal(1, category.DivisionId);
            Assert.Equal(1, category.ProductId);
        }
    }
}
