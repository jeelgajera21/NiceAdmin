using System.ComponentModel.DataAnnotations;

namespace Web_Api.Model
{
    public class ProductModel
    {
        
        public int? ProductID {  get; set; }

        [Required(ErrorMessage = "Product Name is not Entered")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Product Code is not Entered")]
        public string ProductCode { get; set; }

        [Required(ErrorMessage = "Product Price is not Entered")]
        public Decimal ProductPrice { get; set; }

        [Required(ErrorMessage = "Product Discription is not Entered")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Product UserID is not Entered")]
        public int UserID { get; set; }  
        public string UserName { get; set; }  
    }

    public class ProductDropDownModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
    }
}
