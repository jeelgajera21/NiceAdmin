using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace lab_testing.Model
{
    [Table("Departments")]
    public class Department
    {
        #region Properties

        [Key]
        public int DepartmentId { get; set; }

        
        public string DepartmentName { get; set; }
        #endregion
    }
}
