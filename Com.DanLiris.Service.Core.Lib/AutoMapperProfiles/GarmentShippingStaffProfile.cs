using AutoMapper;
using Com.DanLiris.Service.Core.Lib.Models;
using Com.DanLiris.Service.Core.Lib.ViewModels;

namespace Com.DanLiris.Service.Core.Lib.AutoMapperProfiles
{
    public class GarmentShippingStaffProfile : Profile
    {
        public GarmentShippingStaffProfile()
        {
            CreateMap<GarmentShippingStaffModel, GarmentShippingStaffViewModel>()
                .ReverseMap();
        }
    }
}
