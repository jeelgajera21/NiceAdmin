using System.ComponentModel.DataAnnotations;

namespace Web_Api.Model
{
    public class UserModel
    {
        public int? UserID { get; set; }

        [Required(ErrorMessage = "User Name Is Not Entered")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email Is Not Entered")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Is Not Entered")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Mobile Number Is Not Entered")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Address Is Not Entered")]
        public string Address { get; set; }

        public Boolean IsActive { get; set; }
    }

    public class UserDropDownModel
    {
        public int UserID { get; set; }

        [Required(ErrorMessage ="Plase Select UserName in Drop Down")]
        public string UserName { get; set; }
    }


    public class UserLoginModel
    {
        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }


    public class UserRegisterModel
    {
        public int? UserID { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile Number is required.")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }
    }
}
