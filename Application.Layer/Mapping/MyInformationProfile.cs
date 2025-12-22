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

            CreateMap<CreateMyInformationDto, MyInformation>()
                .ForMember(dest => dest.ProfileImage,
                    opt => opt.Ignore());

            CreateMap<MyInformation, EditMyInformationDto>().ReverseMap();

            CreateMap<EditMyInformationDto, MyInformation>()
                .ForMember(dest => dest.ProfileImage,
                    opt => opt.Ignore());
        }
    }
}
