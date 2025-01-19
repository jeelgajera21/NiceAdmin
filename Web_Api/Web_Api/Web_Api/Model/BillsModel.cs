using System.ComponentModel.DataAnnotations;

namespace Web_Api.Model
{
    public class BillsModel
    {
        public int? BillID { get; set; }


        [Required(ErrorMessage = "Bill Number is not Entered")]
        public string BillNumber { get; set; }

        [Required(ErrorMessage = "Bill Date is not Entered")]
        public DateTime BillDate { get; set; }

        [Required(ErrorMessage = "OrderID is not Entered")]
        public int OrderID { get; set; }

        [Required(ErrorMessage = "Total Amount is not Entered")]
        public Decimal TotalAmount { get; set; }

        [Required(ErrorMessage = "Discount is not Entered")]
        public Decimal Discount { get; set; }

        [Required(ErrorMessage = "Net Amount is not Entered")]
        public Decimal NetAmount { get; set; }

        [Required(ErrorMessage = "User ID is not Entered")]
        public int UserId { get; set; }

        public string UserName { get; set; }
    }
}
