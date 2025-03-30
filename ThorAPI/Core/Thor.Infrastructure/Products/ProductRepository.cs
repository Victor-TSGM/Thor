using Thor.Domain.Entities;
using Thor.Domain.Interfaces;

namespace Thor.Infrastructure.Products;

public class ProductRepository : IProductRepository
{

    //DBContext
    public Task<Product> Add(Product product)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Product> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Product> Update(Product product)
    {
        throw new NotImplementedException();
    }
}
