using AutoMapper;
using Com.DanLiris.Service.Core.Lib.Models;
using Com.DanLiris.Service.Core.Lib.ViewModels;

namespace Com.DanLiris.Service.Core.Lib.AutoMapperProfiles
{
    public class GarmentCourierProfile : Profile
    {
        public GarmentCourierProfile()
        {
            CreateMap<GarmentCourierModel, GarmentCourierViewModel>()
                .ReverseMap();
        }
    }
}
