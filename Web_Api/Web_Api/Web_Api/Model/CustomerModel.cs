using System.ComponentModel.DataAnnotations;

namespace Web_Api.Model
{
    public class CustomerModel
    {
        public int? CustomerID {  get; set; }    
        [Required(ErrorMessage ="Customer Name Is Not Entered")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Home Address Is Not Entered")]
        public string HomeAddress { get; set; }

        [Required(ErrorMessage = "Email Is Not Entered")]

        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile Number Is Not Entered")]
        public string MobileNo { get; set; }


        [Required(ErrorMessage = "GSTNO Is Not Entered")]
        public string GST_NO { get; set; }


        [Required(ErrorMessage = "City Name Is Not Entered")]

        public string CityName { get; set; }


        [Required(ErrorMessage = "Pin Code Is Not Entered")]

        public string PinCode { get; set; }


        [Required(ErrorMessage = "Net Amount Is Not Entered")]

        public double NetAmount { get; set; }


        [Required(ErrorMessage = "User ID Is Not Entered")]

        public int? UserID { get; set; }
        public string UserName { get; set; }


    }

    public class CustomerDropDownModel
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
    }
}
