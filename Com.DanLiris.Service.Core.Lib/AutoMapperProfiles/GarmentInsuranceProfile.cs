using AutoMapper;
using Com.DanLiris.Service.Core.Lib.Models;
using Com.DanLiris.Service.Core.Lib.ViewModels;

namespace Com.DanLiris.Service.Core.Lib.AutoMapperProfiles
{
    public class GarmentInsuranceProfile : Profile
    {
        public GarmentInsuranceProfile()
        {
            CreateMap<GarmentInsuranceModel, GarmentInsuranceViewModel>()
                .ReverseMap();
        }
    }
}
