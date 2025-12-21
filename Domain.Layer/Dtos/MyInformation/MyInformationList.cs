using System.ComponentModel.DataAnnotations;

namespace Domain.Layer.Dtos.MyInformation
{
    public class MyInformationList 
    {
        #region Properties

        public long Id { get; set; }

        public string HeaderTitle { get; set; }
        public string MainTitle { get; set; }
        public string Description { get; set; }

        public string? ProfileImage { get; set; }

        public string License { get; set; }
        public string CompletedProject { get; set; }
        public string CooperatingCompany { get; set; }
        public string WorkExperience { get; set; }

        public string CreateDateTime { get; set; }

        #endregion
    }
}
