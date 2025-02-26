using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace lab_testing.Model
{
    [Table("Staff")]
    public class Staff
    {
        #region Properties

        [Key]
        public int StaffID { get; set; }

        [Required]
        [StringLength(100)]
        public string StaffName { get; set; }


        #endregion
    }
}
