using Thor.Domain.Commom;

namespace Thor.Domain.Entities;

public class Product
{
    public int Id { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public Department Department { get; private set; }
    public string ImageURL { get; private set; }

    public Product()
    {
        
    }

    public static Result<Product> New(
        int id,
        string description,
        decimal price
        )
    {
        Product entity = new Product()
        {
            Id = id,
            Description = description,
            Price = price
        };

        //Check Rules

        return Result<Product>.Success(entity);
    }

    public Result<string> AddImage(string imageURL)
    {
        if(string.IsNullOrEmpty(imageURL))
            return Result<string>.Fail("A URL ou caminho da imagem é requerido");

        this.ImageURL = imageURL;
        return Result<string>.Success("Imagem associada com sucesso!");
    }

    public Result AddDepartment(Department department)
    {
        if (department == null)
            return Result.Fail("Departamento é requerido");
        this.Department = department;
        return Result.Success("Departamento associado com sucesso!");
    }

    public Result Update(string? description, decimal? price)
    {
        if (description == null && price == null)
            return Result.Fail("Nada foi informado para atualizar");

        if (description != null)
            this.Description = description;
        if (price != null)
            this.Price = price.Value;

        return Result.Success("Produto atualizado com sucesso!");

    }
}
