using System.ComponentModel.DataAnnotations;

namespace Domain.Layer.Dtos.Account
{
    public class ForgotPasswordDto 
    {
        [Display(Name = "تلفن همراه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(11, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "فقط اعداد مجاز می باشد")]
        public string Mobile { get; set; }

        
    }
    public enum ForgotPasswordResult
    {
        Success,
        Error,
        NotFound
    }
}
