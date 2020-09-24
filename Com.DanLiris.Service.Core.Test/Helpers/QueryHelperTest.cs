using Com.DanLiris.Service.Core.Lib.Helpers;
using Com.DanLiris.Service.Core.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.DanLiris.Service.Core.Test.Helpers
{
    public class QueryHelperTest
    {
        [Fact]
        public void Filter_Success()
        {
            var query = new List<Quality>()
            {
                new Quality()
                {
                    Name ="value"
                }
            }.AsQueryable();

            Dictionary<string, object> filterDictionary = new Dictionary<string, object>();
            filterDictionary.Add("Name", "value");

            var result = QueryHelper<Quality>.Filter(query, filterDictionary);
            Assert.NotNull(result);
            Assert.True(0 < result.Count());
        }

        [Fact]
        public void Order_Success()
        {
            var query = new List<Quality>()
            {
                new Quality()
                {
                    Name ="value"
                }
            }.AsQueryable();

            Dictionary<string, string> orderDictionary = new Dictionary<string, string>();
            orderDictionary.Add("Name", "desc");
            var result = QueryHelper<Quality>.Order(query, orderDictionary);

            Assert.True(0 < result.Count());
            Assert.NotNull(result);

        }

        [Fact]
        public void Search_Success()
        {
            var query = new List<Quality>()
            {
                new Quality()
                {
                    Name ="value"
                }
            }.AsQueryable();

            List<string> searchAttributes = new List<string>()
            {
                "Name"
            };

            var result = QueryHelper<Quality>.Search(query, searchAttributes, "value", true);
            Assert.NotNull(result);
            Assert.True(0 < result.Count());
        }
    }
}
