using Thor.Domain.Entities;

namespace Thor.Domain.Interfaces;

public interface IDepartmentRepository
{
    Task<IEnumerable<Department>> GetAll();
    Task<Department> GetById(Guid id);
    Task<Department> Add(Department department);
    Task<Department> Update(Department department);
    Task<bool> Delete(Guid id);
}
