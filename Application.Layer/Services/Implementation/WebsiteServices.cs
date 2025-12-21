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

    public async Task<List<MyInformationList>> GetAllMyInformation()
    {

        return await _myInformationRepository
            .GetQuery()
            .ProjectTo<MyInformationList>(_mapper.ConfigurationProvider)
            .ToListAsync();

        //return await _myInformationRepository.GetQuery()
        //    .AsQueryable()
        //    .Where(x => !x.IsDelete)
        //    .Select(x => new MyInformationList
        //    {
        //        Id = x.Id,
        //        HeaderTitle = x.HeaderTitle,
        //        MainTitle = x.MainTitle,
        //        ProfileImage = x.ProfileImage,
        //        CreateDateTime = x.CreateDate.ToStringShamsiDate()

        //    }).ToListAsync();
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

        var newInfo = new MyInformation
        {
            HeaderTitle = myInfo.HeaderTitle,
            MainTitle = myInfo.MainTitle,
            ProfileImage = imageName,
            CompletedProject = myInfo.CompletedProject,
            CooperatingCompany = myInfo.CooperatingCompany,
            Description = myInfo.Description,
            License = myInfo.License,
            WorkExperience = myInfo.WorkExperience,

        };

        await _myInformationRepository.AddEntity(newInfo);
        await _myInformationRepository.SaveChanges();

        return CreateMyInformationResult.Success;


    }

    public Task<EditMyInformationDto> GetMyInformationForEdit(long myInfoId)
    {
        throw new NotImplementedException();
    }

    public Task<EditMyInformationResult> EditMyInformation(EditMyInformationDto myInfo, IFormFile profileImage)
    {
        throw new NotImplementedException();
    }

    #endregion


    #region Dispose

    public async ValueTask DisposeAsync()
    {
        // TODO release managed resources here
    }

    #endregion

   
}