using AutoMapper;
using Domain.Layer.Dtos.MyInformation;
using Domain.Layer.Entities.MyInformation;

namespace Application.Layer.Mapping
{
    public class MyInformationProfile : Profile
    {
        public MyInformationProfile()
        {
            CreateMap<MyInformation, MyInformationList>().ReverseMap();
        }
    }
}
