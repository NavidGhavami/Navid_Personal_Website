

namespace Domain.Layer.Dtos.MyInformation
{
    public class EditMyInformationDto : CreateMyInformationDto
    {
        public long Id { get; set; }
    }

    public enum EditMyInformationResult
    {
        Success,
        Error,
        NotFound
    }
}
