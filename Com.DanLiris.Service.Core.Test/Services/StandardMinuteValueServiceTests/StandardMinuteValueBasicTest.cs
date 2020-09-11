using Com.Danliris.Service.Core.Test.Helpers;
using Com.DanLiris.Service.Core.Lib;
using Com.DanLiris.Service.Core.Lib.Services;
using Com.DanLiris.Service.Core.Test.DataUtils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xunit;
using Models = Com.DanLiris.Service.Core.Lib.Models;

namespace Com.DanLiris.Service.Core.Test.Services.StandardMinuteValueServiceTests
{
    [Collection("ServiceProviderFixture Collection")]
    public class StandardMinuteValueBasicTest : BasicServiceTest<CoreDbContext, StandardMinuteValueService, Models.StandardMinuteValue>
    {
        private static readonly string[] createAttrAssertions = { "BuyerName", "ComodityName", "SMVCutting", "SMVSewing", "SMVFinishing" };
        private static readonly string[] updateAttrAssertions = { "BuyerName", "ComodityName", "SMVCutting", "SMVSewing", "SMVFinishing" };
        private static readonly string[] existAttrCriteria = { };

        public StandardMinuteValueBasicTest(ServiceProviderFixture fixture) : base(fixture, createAttrAssertions, updateAttrAssertions, existAttrCriteria)
        {
        }

        private StandardMinuteValueDataUtil DataUtil
        {
            get { return (StandardMinuteValueDataUtil)ServiceProvider.GetService(typeof(StandardMinuteValueDataUtil)); }
        }

        private StandardMinuteValueService Services
        {
            get { return (StandardMinuteValueService)ServiceProvider.GetService(typeof(StandardMinuteValueService)); }
        }

        public override void EmptyCreateModel(Models.StandardMinuteValue model)
        {
            model.BuyerName = string.Empty;
            model.ComodityName = string.Empty;
            model.SMVCutting = 0;
            model.SMVFinishing = 0;
            model.SMVSewing = 0;
        }

        public override void EmptyUpdateModel(Models.StandardMinuteValue model)
        {
            model.BuyerName = string.Empty;
            model.ComodityName = string.Empty;
            model.SMVCutting = 0;
            model.SMVFinishing = 0;
            model.SMVSewing = 0;
        }

        public override Models.StandardMinuteValue GenerateTestModel()
        {
            string guid = Guid.NewGuid().ToString();

            return new Models.StandardMinuteValue()
            {
                BuyerId = 1,
                BuyerCode = string.Format("TEST {0}", guid),
                BuyerName = string.Format("TEST {0}", guid),
                ComodityId = 1,
                ComodityCode = string.Format("TEST {0}", guid),
                ComodityName = string.Format("TEST {0}", guid),
                SMVCutting = 1,
                SMVFinishing = 1,
                SMVSewing = 2,
            };
        }

        [Fact]
        public void Should_Success_MapToViewModel()
        {
            Models.StandardMinuteValue model = GenerateTestModel();

            var Response = Services.MapToViewModel(model);
            Assert.NotNull(Response);

        }

       

    }
}
