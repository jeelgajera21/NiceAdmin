using lab_testing.Model;

namespace lab_testing.Repository.Interface
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetDepartments();
        Task<Department> GetDepartment(int departmentId);
        Task<bool> DeleteDepartment(int departmentId);
        Task<Department> UpdateDepartment(Department model);
        Task<Department> AddDepartment(Department model);
    }
}
