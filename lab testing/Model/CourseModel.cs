using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace lab_testing.Model
{
    [Table("Courses")]
    public class Course
    {
        #region Properties

        [Key]
        public int CourseId { get; set; }

        [Required]
        [StringLength(100)]
        public string CourseName { get; set; }

        #endregion
    }
}
