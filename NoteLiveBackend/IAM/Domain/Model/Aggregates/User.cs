using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using NoteLiveBackend.IAM.Domain.Model.Commands;
using NoteLiveBackend.Room.Domain.Model.Entities;

namespace NoteLiveBackend.IAM.Domain.Model.Aggregates
{
public class User
{
    public Guid Id { get; private set; }
    
    [MaxLength(20)]
    public string Username { get; private set; }
    
    [JsonIgnore]
    [MaxLength(100)]
    public string PasswordHash { get; private set; }
    
    [MaxLength(50)]
    public string FirstName { get; private set; }
    
    [MaxLength(50)]
    public string LastName { get; private set; }
    
    [MaxLength(100)]
    public string Email { get; private set; }
    
    [MaxLength(20)]
    public string Role { get; private set; }
    [NotMapped]
    public List<Question> Questions{
        get;
        set;
    }
    // Propiedad de navegación para la relación con Rooms
    [JsonIgnore]
    public ICollection<Room.Domain.Model.Entities.Room> Rooms { get; set; } = new List<Room.Domain.Model.Entities.Room>();
    public User(string username, string passwordHash, string firstName, string lastName, string email, string role = "alumno")
    {
        Id = Guid.NewGuid();
        Username = username.Length <= 20 ? username : throw new ArgumentException("Username must be 20 characters or less.");
        PasswordHash = passwordHash.Length <= 100 ? passwordHash : throw new ArgumentException("Password must be 100 characters or less.");
        FirstName = firstName.Length <= 50 ? firstName : throw new ArgumentException("First name must be 50 characters or less.");
        LastName = lastName.Length <= 50 ? lastName : throw new ArgumentException("Last name must be 50 characters or less.");
        Email = email.Length <= 100 ? email : throw new ArgumentException("Email must be 100 characters or less.");
        Role = role.Length <= 20 ? role : throw new ArgumentException("Role must be 20 characters or less.");
        Questions = new List<Question>();
    }

    public User() : this(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty)
    {
    }

    public User UpdateUsername(string username)
    {
        if (username.Length > 20)
            throw new ArgumentException("Username must be 20 characters or less.");
        
        Username = username;
        return this;
    }

    public User UpdatePasswordHash(string passwordHash)
    {
        if (passwordHash.Length > 100)
            throw new ArgumentException("Password must be 100 characters or less.");
        
        PasswordHash = passwordHash;
        return this;
    }

    public User UpdateFirstName(string firstName)
    {
        if (firstName.Length > 50)
            throw new ArgumentException("First name must be 50 characters or less.");
        
        FirstName = firstName;
        return this;
    }

    public User UpdateLastName(string lastName)
    {
        if (lastName.Length > 50)
            throw new ArgumentException("Last name must be 50 characters or less.");
        
        LastName = lastName;
        return this;
    }

    public User UpdateEmail(string email)
    {
        if (email.Length > 100)
            throw new ArgumentException("Email must be 100 characters or less.");
        
        Email = email;
        return this;
    }

    public User UpdateRole(string role)
    {
        if (role.Length > 20)
            throw new ArgumentException("Role must be 20 characters or less.");
        
        Role = role;
        return this;
    }
}
}