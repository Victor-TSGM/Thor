using Thor.Domain.Commom;

namespace Thor.Domain.Entities;

public class Department
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Department()
    {
        
    }

    public static Result<Department> New(
        int id,
        string name
        )
    {
        Department entity = new Department()
        {
            Id = id,
            Name = name
        };


        //Check Rules
        return Result<Department>.Success(entity);
    }

    public Result Update(string name)
    {
        if (string.IsNullOrEmpty(name))
            return Result.Fail("Nome é requerido");
        this.Name = name;
        return Result.Success("Departamento atualizado com sucesso!");
    }
}
