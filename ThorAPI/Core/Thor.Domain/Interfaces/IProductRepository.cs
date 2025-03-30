
using Thor.Domain.Entities;

namespace Thor.Domain.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAll();
    Task<Product> GetById(Guid id);
    Task<Product> Add(Product product);
    Task<Product> Update(Product product);
    Task<bool> Delete(Guid id);
}
