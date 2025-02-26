using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace lab_testing.Model
{
    [Table("Students")]
    public class Student
    {
        #region Properties

        [Key]
        public int StudentId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        public string Enrollment { get; set; }

        [Required]
        public int Semester { get; set; }

        #endregion
    }
}
