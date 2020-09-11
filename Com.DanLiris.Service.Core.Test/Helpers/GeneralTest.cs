using Com.DanLiris.Service.Core.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.DanLiris.Service.Core.Test.Helpers
{
    public class GeneralTest
    {
        [Fact]
        public void should_success_TransformOrderBy_deleted()
        {
            General.TransformOrderBy("_deleted");
        }

        [Fact]
        public void should_success_TransformOrderBy_active()
        {
            General.TransformOrderBy("_active");
        }

        [Fact]
        public void should_success_TransformOrderBy_createdDate()
        {
            General.TransformOrderBy("_createdDate");
        }

        [Fact]
        public void should_success_TransformOrderBy_createdBy()
        {
            General.TransformOrderBy("_createdBy");
        }

        [Fact]
        public void should_success_TransformOrderBy_createdAgent()
        {
            General.TransformOrderBy("_createdAgent");
        }
        [Fact]
        public void should_success_TransformOrderBy_updatedDate()
        {
            General.TransformOrderBy("_updatedDate");
        }
        [Fact]
        public void should_success_TransformOrderBy_updatedBy()
        {
            General.TransformOrderBy("_updatedBy");
        }

        [Fact]
        public void should_success_TransformOrderBy_updateAgent()
        {
            General.TransformOrderBy("_updateAgent");
        }
    }
}
