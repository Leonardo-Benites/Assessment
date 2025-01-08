using Assessment.API.Models;
using Assessment.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Assessment.Infrastructure.Repositories
{
    public class EmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> GetById(int id)
        {
            return await _context.Employee.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        }
        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _context.Employee.Include(x => x.Department).ToListAsync();
        }
        public async Task Insert(Employee obj)
        {
            _context.Department.Attach(obj.Department = new Department() { Id = obj.DepartmentId });
            _context.Employee.Add(obj);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Employee obj)
        {
            _context.Department.Attach(obj.Department = new Department() { Id = obj.DepartmentId });
            _context.Employee.Update(obj);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(Employee obj)
        {
            _context.Department.Attach(obj.Department = new Department() { Id = obj.DepartmentId });
            _context.Employee.Remove(obj);
            await _context.SaveChangesAsync();
        }
    }
}
