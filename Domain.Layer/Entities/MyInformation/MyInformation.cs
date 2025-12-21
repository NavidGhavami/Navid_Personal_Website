using Domain.Layer.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Layer.Entities.MyInformation
{
    public class MyInformation : BaseEntity
    {
        #region Properties

        [Display(Name = "عنوان هدر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string HeaderTitle { get; set; }

        [Display(Name = "عنوان اصلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(350, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string MainTitle { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(550, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Description { get; set; }

        [Display(Name = "تصویر پروفایل")]
        public string? ProfileImage { get; set; }

        [Display(Name = "تعداد گواهی و دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(5, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string License { get; set; }

        [Display(Name = "پروژه های تکمیل شده")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(5, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string CompletedProject { get; set; }

        [Display(Name = "همکاری با شرکتها")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(5, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string CooperatingCompany { get; set; }

        [Display(Name = "تجربه کاری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(5, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string WorkExperience { get; set; }

        #endregion
    }
}
