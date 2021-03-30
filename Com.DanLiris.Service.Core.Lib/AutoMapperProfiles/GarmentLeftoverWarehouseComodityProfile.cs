using AutoMapper;
using Com.DanLiris.Service.Core.Lib.Models;
using Com.DanLiris.Service.Core.Lib.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.DanLiris.Service.Core.Lib.AutoMapperProfiles
{
    public class GarmentLeftoverWarehouseComodityProfile : Profile
    {
        public GarmentLeftoverWarehouseComodityProfile()
        {
            CreateMap<GarmentLeftoverWarehouseComodityModel, GarmentLeftoverWarehouseComodityViewModel>()
                .ReverseMap();
        }
    }
}
