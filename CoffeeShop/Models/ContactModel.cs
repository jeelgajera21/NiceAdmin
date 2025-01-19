using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Models
{

    public class ContactModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string EnrollmentNo { get; set; }

        /*[Required]
        public string Description { get; set; }*/
    }
}
