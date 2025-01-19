using System.ComponentModel.DataAnnotations;

namespace Web_Api.Model
{
    public class OrderDetailModel
    {
        public int? OrderDetailID { get; set; }

        
        public int OrderID { get; set; }

      
        public int ProductID {  get; set; }

        public string ProductName { get; set; }

        [Required(ErrorMessage = "Quantity Is Not Entered")]
        public int Quantity {  get; set; }

        [Required(ErrorMessage = "Amount Is Not Entered")]
        public double Amount {  get; set; }

        [Required(ErrorMessage = "Total Amount Is Not Entered")]
        public double TotalAmount{ get; set; }
        [Required(ErrorMessage = "User Id Is Not Entered")]
        public int UserID { get; set; }

        public string UserName { get; set; }
    }
}
