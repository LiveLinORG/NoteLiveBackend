using NoteLiveBackend.IAM.Domain.Model.Aggregates;
using NoteLiveBackend.Shared.Domain.Repositories;
using System.Threading.Tasks;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> FindByUsernameAsync(string username);
    bool ExistsByUsername(string username);
    Task<User?> FindByCodigoProfesorAsync(long codigoProfesor);
    Task<User?> FindByCorreoAlumnoAsync(string correo);
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid id);
    Task<IEnumerable<User>> FindByNameAsync(string name);
    Task<User?> FindByNameAndCorreoAsync(string name, string correo);
}