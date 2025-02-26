using lab_testing.Data;
using lab_testing.Model;
using lab_testing.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;

namespace lab_testing.Repository.Services
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext appDbContext;

        public DepartmentRepository(ApplicationDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Department> GetDepartment(int departmentId)
        {
            return await appDbContext.Departments
                .FirstOrDefaultAsync(d => d.DepartmentId == departmentId);
        }

        public async Task<IEnumerable<Department>> GetDepartments()
        {
            return await appDbContext.Departments.ToListAsync();
        }

        public async Task<Department> AddDepartment(Department department)
        {
            var result = await appDbContext.Departments.AddAsync(department);

            await appDbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<Department> UpdateDepartment(Department department)
        {
            var existingDepartment = await appDbContext.Departments
                .FirstOrDefaultAsync(d => d.DepartmentId == department.DepartmentId);

            if (existingDepartment == null) return null;

            existingDepartment.DepartmentName = department.DepartmentName;

            await appDbContext.SaveChangesAsync();

            return existingDepartment;
        }

        public async Task<bool> DeleteDepartment(int departmentId)
        {
            var departmentToDelete = await appDbContext.Departments
                .FirstOrDefaultAsync(d => d.DepartmentId == departmentId);

            if (departmentToDelete == null) return false;

            appDbContext.Departments.Remove(departmentToDelete);

            await appDbContext.SaveChangesAsync();

            return true;
        }
    }
}
