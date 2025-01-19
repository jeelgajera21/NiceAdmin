using System.ComponentModel.DataAnnotations;

namespace Web_Api.Model
{
    public class OrderModel
    {
        public int? OrderID { get; set; }  

        [Required(ErrorMessage ="Order Date Is Not Entered")]
        public DateTime OrderDate {  get; set; }

        [Required(ErrorMessage = "Customer ID Is Not Entered")]
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }


        [Required(ErrorMessage = "Order Number Is Not Entered")]
        public string OrderNumber { get; set; }

        public string PaymentMode { get; set; }

        [Required(ErrorMessage = "Total Amount Is Not Entered")]
        public Decimal TotalAmount { get; set; }

       

        [Required(ErrorMessage = "Shipping Address Is Not Entered")]
        public string ShippingAddress { get; set; }

        [Required(ErrorMessage = "User ID Is Not Entered")]
        public int UserID {  get; set; }
        public string UserName { get; set; }

    }

    public class OrderDropDownModel
    {
        public int OrderID { get; set; }
        public string OrderNumber{ get; set; }
    }
}
