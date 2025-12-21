using Domain.Layer.Dtos.MyInformation;
using Microsoft.AspNetCore.Http;

namespace Application.Layer.Services.Interface;

public interface IWebsiteServices : IAsyncDisposable
{

    #region My Information

    Task<List<MyInformationList>> GetAllMyInformation();
    Task<CreateMyInformationResult> CreateMyInformation(CreateMyInformationDto myInfo, IFormFile profileImage);
    Task<EditMyInformationDto> GetMyInformationForEdit(long myInfoId);
    Task<EditMyInformationResult> EditMyInformation(EditMyInformationDto myInfo, IFormFile profileImage);

    #endregion

}