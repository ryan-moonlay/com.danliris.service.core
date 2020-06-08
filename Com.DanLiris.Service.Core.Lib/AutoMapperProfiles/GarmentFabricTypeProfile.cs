using AutoMapper;
using Com.DanLiris.Service.Core.Lib.Models;
using Com.DanLiris.Service.Core.Lib.ViewModels;

namespace Com.DanLiris.Service.Core.Lib.AutoMapperProfiles
{
    public class GarmentFabricTypeProfile : Profile
    {
        public GarmentFabricTypeProfile()
        {
            CreateMap<GarmentFabricTypeModel, GarmentFabricTypeViewModel>()
                .ReverseMap();
        }
    }
}
