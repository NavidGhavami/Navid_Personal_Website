using System.ComponentModel.DataAnnotations;

namespace Domain.Layer.Dtos.Account
{

    public class RegisterUserDto 
    {
        public long RoleId { get; set; }
        [Display(Name = "تلفن همراه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(11, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "فقط اعداد مجاز می باشد")]
        public string Mobile { get; set; }

        [Display(Name = "ایمیل")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [EmailAddress(ErrorMessage = "فرمت ایمیل صحیح نمی باشد")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Display(Name = "کد فعالسازی")]
        public int? ActivationCode { get; set; }


        [Display(Name = "کلمه ی عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Password { get; set; }

        [Display(Name = "کلمه ی عبور")]
        [Required(ErrorMessage = "لطفا تکرار {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Compare("Password", ErrorMessage = "کلمه های عبور مغایرت دارند")]
        public string ConfirmPassword { get; set; }

        public List<long> RoleSelectedCategories { get; set; } = new List<long>
            {
                2
            };
    }

    public enum RegisterUserResult
    {
        Success,
        MobileExists,
        Error
    }

}

