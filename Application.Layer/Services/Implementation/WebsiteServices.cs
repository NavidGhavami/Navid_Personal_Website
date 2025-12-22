using Application.Layer.Extensions;
using Application.Layer.Services.Interface;
using Application.Layer.Utilities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Layer.Dtos.MyInformation;
using Domain.Layer.Entities.MyInformation;
using Domain.Layer.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Layer.Services.Implementation;

public class WebsiteServices : IWebsiteServices
{


    #region Constructor

    private readonly IMapper _mapper;
    private readonly IGenericRepository<MyInformation> _myInformationRepository;

    public WebsiteServices(IMapper mapper, IGenericRepository<MyInformation> myInformationRepository)
    {
        _mapper = mapper;
        _myInformationRepository = myInformationRepository;
        
    }

    #endregion


    #region My Information

    public async Task<MyInformationList> GetAllMyInformation()
    {
        return await _myInformationRepository
            .GetQuery()
            .ProjectTo<MyInformationList>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync() ?? new MyInformationList();

    }

    public async Task<CreateMyInformationResult> CreateMyInformation(CreateMyInformationDto myInfo, IFormFile profileImage)
    {
        if (profileImage == null || !profileImage.IsImage())
        {
            return CreateMyInformationResult.Error;
        }

        var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(profileImage.FileName);
        profileImage.AddImageToServer(imageName, PathExtension.UserProfileOriginServer,
            100, 100, PathExtension.UserProfileThumbServer);

        var newInfo = _mapper.Map<MyInformation>(myInfo);

        newInfo.ProfileImage = imageName;

        await _myInformationRepository.AddEntity(newInfo);
        await _myInformationRepository.SaveChanges();

        return CreateMyInformationResult.Success;
    }

    public async Task<EditMyInformationDto> GetMyInformationForEdit(long myInfoId)
    {
        return await _myInformationRepository
            .GetQuery()
            .ProjectTo<EditMyInformationDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == myInfoId)
            ?? new EditMyInformationDto();
    }

    public async Task<EditMyInformationResult> EditMyInformation(EditMyInformationDto myInfo, IFormFile profileImage)
    {
        var mainInfo = await _myInformationRepository
            .GetQuery()
            .SingleOrDefaultAsync(x => x.Id == myInfo.Id);

        if (mainInfo == null)
        {
            return EditMyInformationResult.NotFound;
        }

        _mapper.Map(myInfo, mainInfo);

        if (profileImage != null && profileImage.IsImage())
        {
            var imageName = Guid.NewGuid().ToString("N") +
                            Path.GetExtension(profileImage.FileName);

            profileImage.AddImageToServer(
                imageName,
                PathExtension.UserProfileOriginServer,
                100,
                100,
                PathExtension.UserProfileThumbServer);

            mainInfo.ProfileImage = imageName;
        }

        _myInformationRepository.EditEntity(mainInfo);
        await _myInformationRepository.SaveChanges();

        return EditMyInformationResult.Success;
    }


    #endregion


    #region Dispose

    public async ValueTask DisposeAsync()
    {
        // TODO release managed resources here
    }

    #endregion

   
}